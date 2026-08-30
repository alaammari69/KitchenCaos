using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateScript : KitchenObject, IKitchenObjectParent {
    [SerializeField] private Transform holdPosition;
    [SerializeField] private KitchenObjectSO burgerSO;
    public KitchenObjectSO BurgerSO => burgerSO;
    public void ClearChildKitchenObjects() {
        foreach (Transform kitchenObject in holdPosition) {
            Destroy(kitchenObject.gameObject);
        }
    }

    public Transform GetChildKitchenObject() {
        return holdPosition.GetChild(0);
    }

    public Transform GetKitchenObjectFollowTransform() {
        return holdPosition;
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

    public bool HasBurger(out BurgerScript burgerScript) {
        if (HasChildKitchenObject() && GetChildKitchenObject().TryGetComponent<BurgerScript>(out burgerScript)) {
            return true;
        }
        burgerScript = null;
        return false;
    }
}
