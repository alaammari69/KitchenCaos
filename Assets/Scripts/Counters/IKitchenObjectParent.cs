using UnityEngine;

public interface IKitchenObjectParent {
    public bool HasChildKitchenObject();
    public void SetChildKitchenObject(Transform kitchenObject);
    public Transform TakeChildKitchenObject();
    public Transform GetKitchenObjectFollowTransform();
    public Transform GetChildKitchenObject();
    public void ClearChildKitchenObjects();
}
