using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public CollidersList interactColliders;

    CharacterController characterController;
    Vector2 moveInput;
    Player player;
    Collider _collider;
    BaseInteractable currentlySelectedInteractable;

    void Awake() {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();
        _collider = GetComponent<Collider>();
    }

    void Update() {
        if (player.playerState != PlayerState.Free) return;
        Interact();
        Move();
    }

    void Move() {
        moveInput = InputManager.move;
        Vector3 forward = Camera.main.transform.forward, right = Camera.main.transform.right;
        forward.y = 0; forward.Normalize();
        right.y = 0; right.Normalize();
        Vector3 characterMoveInput = forward * moveInput.y + right * moveInput.x;
        characterController.Move(characterMoveInput.normalized * (speed * Time.deltaTime));
        transform.Rotate(Vector3.up, Vector3.SignedAngle(transform.forward, characterMoveInput, Vector3.up));
        StickToGround();
    }

    void StickToGround() {
        Ray ray = new(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5)) {
            transform.position = hitInfo.point + transform.up * (_collider.bounds.size.y / 2);
        }
    }

    void Interact() {
        if (player.playerState != PlayerState.Free) return;
        List<GameObject> interactableObjects = interactColliders.GetObjects();
        GameObject selectedObject = interactableObjects
            .OrderBy(obj => Vector3.Distance(obj.transform.position, transform.position))
            .FirstOrDefault();
        BaseInteractable selectedInteractable = selectedObject == null ? null : selectedObject.GetComponent<BaseInteractable>();
        if (selectedInteractable != currentlySelectedInteractable) {
            if (currentlySelectedInteractable != null && currentlySelectedInteractable.TryGetComponent(out Outline outline1)) outline1.enabled = false;
            if (selectedInteractable != null && selectedInteractable.TryGetComponent(out Outline outline2)) outline2.enabled = true;
            currentlySelectedInteractable = selectedInteractable;
        }
        if (currentlySelectedInteractable != null && InputManager.ConsumeInteract()) currentlySelectedInteractable.Interact();
    }
}

// for objects which interactions can be started by player interaction
interface IPlayerInteractable {}
