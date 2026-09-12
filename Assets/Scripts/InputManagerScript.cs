using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerScript : MonoBehaviour {
    private InputActions inputActions;
    public event EventHandler OnPlayerInteractPerformed;
    public event EventHandler OnPlayerInteractAlternatePerformed;
    public event EventHandler OnPausePerformed;
    void OnDisable() {
        inputActions.Player.Interact.performed -= InvokeOnPlayerInteractPerformed;
        inputActions.Player.InteractAlternate.performed -= InvokeOnPlayerInteractAlternatePerformed;
        inputActions.Player.Pause.performed -= InvokeOnPausePerformed;
        inputActions.Player.Disable();
    }
    void Awake() {
        inputActions = new InputActions();
    }
    // Start is called before the first frame update
    void Start() {
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += InvokeOnPlayerInteractPerformed;
        inputActions.Player.InteractAlternate.performed += InvokeOnPlayerInteractAlternatePerformed;
        inputActions.Player.Pause.performed += InvokeOnPausePerformed;
    }
    void OnDestroy() {
        inputActions.Dispose();
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
    private void InvokeOnPausePerformed(InputAction.CallbackContext context) {
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }
}
