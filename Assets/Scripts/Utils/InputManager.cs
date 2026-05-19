using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(PlayerInput))]
public class InputManager : SingletonMonoBehaviour<InputManager>
{
    public static Vector2 move;
    public static bool interact;

    public void OnMove(InputAction.CallbackContext input) { move = input.ReadValue<Vector2>(); }
    public void OnInteract(InputAction.CallbackContext input) { interact = input.performed; }

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static bool ConsumeInteract() {
        bool oldInteract = interact;
        interact = false;
        return oldInteract;
    }
}
