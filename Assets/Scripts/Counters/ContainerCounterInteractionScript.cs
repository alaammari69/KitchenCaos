using System;
using UnityEngine;

public class ContainerCounterInteractionScript : CounterInteractionScript {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    protected override void InteractWithCounter(object o, EventArgs args) {
        Debug.Log(playerInRange.transform.name + "IS INTERACTING WITH " + transform.name);

        PlayerController playerController = playerInRange.GetComponent<PlayerController>();

        if (playerController.HasChildKitchenObject()) {
            Debug.Log("player is holding something");
            return;
        }
        else {
            Debug.Log("kitchen object instantiated");
            GameObject kitchenObject = Instantiate(kitchenObjectSO.prefab);
            playerController.SetChildKitchenObject(kitchenObject.transform);
            counterEventBus.InvokeOnPlayerInteracted(playerController);
        }

    }
}
