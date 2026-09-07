using UnityEngine;

public class PlayerController : MonoBehaviour, IKitchenObjectParent {
  [SerializeField] private InputManagerScript inputManagerScript;
  public PlayerEventBus playerEventBus { get; private set; }
  private PlayerMovementScript playerMovementScript;
  private Vector2 input = Vector2.zero;
  [SerializeField] private Transform holdPosition;

  void Awake() {
    playerEventBus = GetComponent<PlayerEventBus>();
    playerMovementScript = GetComponent<PlayerMovementScript>();
  }
  void Start() {
    inputManagerScript.OnPlayerInteractPerformed += playerEventBus.InvokeOnPlayerInteract;
    inputManagerScript.OnPlayerInteractAlternatePerformed += playerEventBus.InvokeOnPlayerInteractAlternate;
  }
  void OnDisable() {
    inputManagerScript.OnPlayerInteractPerformed -= playerEventBus.InvokeOnPlayerInteract;
    inputManagerScript.OnPlayerInteractAlternatePerformed -= playerEventBus.InvokeOnPlayerInteractAlternate;
  }

  void FixedUpdate() {
    UpdateMovement();
  }



  private void UpdateMovement() {
    input = inputManagerScript.GetPlayerMovementVectorNormalized();
    playerMovementScript.MoveTowards(input);
  }


  public bool HasChildKitchenObject() {
    return holdPosition.childCount != 0;
  }
  public void SetChildKitchenObject(Transform kitchenObject) {
    kitchenObject.SetParent(holdPosition, false);
    kitchenObject.localPosition = Vector3.zero;
    kitchenObject.localRotation = Quaternion.identity;
  }
  public Transform TakeChildKitchenObject() {
    Transform kitchenObject = GetChildKitchenObject();
    holdPosition.DetachChildren();
    return kitchenObject;
  }
  public Transform GetChildKitchenObject() {
    return holdPosition.GetChild(0);
  }
  public Transform GetKitchenObjectFollowTransform() {
    return holdPosition;
  }
  public void ClearChildKitchenObjects() {
    foreach (Transform kitchenObject in holdPosition) {
      Destroy(kitchenObject.gameObject);
    }
  }
}
