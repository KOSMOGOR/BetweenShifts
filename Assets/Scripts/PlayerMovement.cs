using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Camera mainCam;
    CharacterController characterController;
    Vector2 moveInput;

    void Awake() {
        mainCam = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

#pragma warning disable IDE0051
    void OnMove(InputValue input) {
        moveInput = input.Get<Vector2>();
    }
#pragma warning restore IDE0051

    void Update() {
        Vector3 forward = mainCam.transform.forward, right = mainCam.transform.right;
        forward.y = 0; forward.Normalize();
        right.y = 0; right.Normalize();
        Vector3 characterMoveInput = forward * moveInput.y + right * moveInput.x;
        characterController.Move(characterMoveInput.normalized * (speed * Time.deltaTime));
    }
}
