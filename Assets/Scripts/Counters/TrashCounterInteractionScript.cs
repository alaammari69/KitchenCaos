using System;
using UnityEngine;

public class TrashCounterInteractionScript : CounterInteractionScript {
    protected override void InteractWithCounter(object o, EventArgs args) {
        Debug.Log(playerInRange.transform.name + "IS INTERACTING WITH " + transform.name);


        PlayerController playerController = playerInRange.GetComponent<PlayerController>();
        counterEventBus.InvokeOnPlayerInteracted(playerController);
        playerController.ClearChildKitchenObjects();
    }
}
