using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public CollidersList interactColliders;

    Camera mainCam;
    CharacterController characterController;
    Vector2 moveInput;
    Player player;
    Collider _collider;

    void Awake() {
        mainCam = Camera.main;
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
        Vector3 forward = mainCam.transform.forward, right = mainCam.transform.right;
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
        if (!InputManager.interact) return;
        List<GameObject> interactableObjects = interactColliders.GetObjects();
        if (interactableObjects.Count == 0) return;
        interactableObjects
            // .Where(obj => obj.GetComponent<BaseInteractable>() is IPlayerInteractable)
            .OrderBy(obj => Vector3.Distance(obj.transform.position, transform.position))
            .First()
            .GetComponent<BaseInteractable>().Interact();
    }
}

// for object which interactions can be started by player interaction
interface IPlayerInteractable {}
