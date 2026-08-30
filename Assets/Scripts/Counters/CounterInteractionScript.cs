using System;
using UnityEngine;

public class CounterInteractionScript : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] protected Transform aboveTablePosition;
    protected CounterEventBus counterEventBus;
    protected virtual void Awake() {
        counterEventBus = GetComponent<CounterEventBus>();
    }
    protected virtual void OnEnable() {
        counterEventBus.OnPlayerInRange += PrepareIntercationOption;
        counterEventBus.OnPlayerOutOfRange += RemoveIntercationOption;
    }
    protected virtual void OnDisable() {
        counterEventBus.OnPlayerInRange -= PrepareIntercationOption;
        counterEventBus.OnPlayerOutOfRange -= RemoveIntercationOption;
    }
    protected virtual void PrepareIntercationOption(object o, PlayerController player) {
        player.GetComponent<PlayerEventBus>().OnPlayerInteract += InteractWithCounter;
        player.GetComponent<PlayerEventBus>().OnPlayerInteractAlternate += InteractAlternateWithCounter;
        playerInRange = player;
    }

    protected virtual void RemoveIntercationOption(object o, PlayerController player) {
        player.GetComponent<PlayerEventBus>().OnPlayerInteract -= InteractWithCounter;
        player.GetComponent<PlayerEventBus>().OnPlayerInteractAlternate -= InteractAlternateWithCounter;
        playerInRange = null;
    }
    protected PlayerController playerInRange = null;
    protected virtual void InteractWithCounter(object o, EventArgs args) {
        Debug.Log(playerInRange.transform.name + "IS INTERACTING WITH " + transform.name);
    }
    protected virtual void InteractAlternateWithCounter(object o, EventArgs args) {
        counterEventBus.InvokeOnInteractedAlternate();
        Debug.Log(playerInRange.transform.name + "IS INTERACTING ALTERNATE WITH " + transform.name);
    }


    public bool HasChildKitchenObject() {
        return aboveTablePosition.childCount != 0;
    }
    public void SetChildKitchenObject(Transform kitchenObject) {
        kitchenObject.SetParent(aboveTablePosition, false);
        kitchenObject.localPosition = Vector3.zero;
        kitchenObject.localRotation = Quaternion.identity;
    }
    public Transform TakeChildKitchenObject() {
        Transform kitchenObject = GetChildKitchenObject();
        aboveTablePosition.DetachChildren();
        return kitchenObject;

    }
    public Transform GetChildKitchenObject() {
        return aboveTablePosition.GetChild(0);
    }
    public Transform GetKitchenObjectFollowTransform() {
        return aboveTablePosition;
    }
    public void ClearChildKitchenObjects() {
        foreach (Transform kitchenObject in aboveTablePosition) {
            Destroy(kitchenObject.gameObject);
        }
    }
}
