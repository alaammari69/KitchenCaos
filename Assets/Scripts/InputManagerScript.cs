using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerScript : MonoBehaviour {
    private InputActions inputActions;
    public event EventHandler OnPlayerInteractPerformed;
    public event EventHandler OnPlayerInteractAlternatePerformed;
    void OnDisable() {
        inputActions.Player.Interact.performed -= InvokeOnPlayerInteractPerformed;
        inputActions.Player.InteractAlternate.performed -= InvokeOnPlayerInteractAlternatePerformed;
    }
    void Awake() {
        inputActions = new InputActions();
    }
    // Start is called before the first frame update
    void Start() {
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += InvokeOnPlayerInteractPerformed;
        inputActions.Player.InteractAlternate.performed += InvokeOnPlayerInteractAlternatePerformed;
    }

    public Vector2 GetPlayerMovementVectorNormalized() {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }
    private void InvokeOnPlayerInteractPerformed(InputAction.CallbackContext context) {
        OnPlayerInteractPerformed?.Invoke(this, EventArgs.Empty);
    }
    private void InvokeOnPlayerInteractAlternatePerformed(InputAction.CallbackContext context) {
        OnPlayerInteractAlternatePerformed?.Invoke(this, EventArgs.Empty);
    }
}
