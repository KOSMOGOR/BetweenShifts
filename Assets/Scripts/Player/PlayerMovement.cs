using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public CollidersList interactColliders;

    Camera mainCam;
    CharacterController characterController;
    Vector2 moveInput;
    Player player;

    void Awake() {
        mainCam = Camera.main;
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();
    }

    void OnEnable() { InputManager.I.Subscribe(gameObject); }
    void OnDisable() { InputManager.I.Unsubscribe(gameObject); }

#pragma warning disable IDE0051, IDE0060
    void OnMove(InputValue input) {
        // moveInput = player.playerState == PlayerState.Free ? input.Get<Vector2>() : Vector2.zero;
        moveInput = input.Get<Vector2>();
    }

    void OnInteract(InputValue input) {
        if (player.playerState != PlayerState.Free) return;
        List<GameObject> interactableObjects = interactColliders.GetObjects();
        if (interactableObjects.Count == 0) return;
        interactableObjects
            // .Where(obj => obj.TryGetComponent<InteractableObject>(out var io))
            .OrderBy(obj => Vector3.Distance(obj.transform.position, transform.position))
            .First()
            .GetComponent<BaseInteractable>().Interact();
    }
#pragma warning restore IDE0051, IDE0060

    void Update() {
        if (player.playerState != PlayerState.Free) return;
        Vector3 forward = mainCam.transform.forward, right = mainCam.transform.right;
        forward.y = 0; forward.Normalize();
        right.y = 0; right.Normalize();
        Vector3 characterMoveInput = forward * moveInput.y + right * moveInput.x;
        characterController.Move(characterMoveInput.normalized * (speed * Time.deltaTime));
        transform.Rotate(Vector3.up, Vector3.SignedAngle(transform.forward, characterMoveInput, Vector3.up));
    }
}

// for object which interactions can be started by player interaction
interface IPlayerInteractable {}
