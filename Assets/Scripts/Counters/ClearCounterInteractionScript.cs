using System;
using UnityEngine;

public class ClearCounterInteractionScript : CounterInteractionScript {
    protected override void InteractWithCounter(object o, EventArgs args) {
        Debug.Log(playerInRange.transform.name + "IS INTERACTING WITH " + transform.name);

        PlayerController playerController = playerInRange.GetComponent<PlayerController>();

        counterEventBus.InvokeOnPlayerInteracted(playerController);
        if (!playerController.HasChildKitchenObject()) {
            if (HasChildKitchenObject()) {
                playerController.SetChildKitchenObject(TakeChildKitchenObject());
            }
        }
        else {
            if (!HasChildKitchenObject()) {
                SetChildKitchenObject(playerController.TakeChildKitchenObject());
            }
        }
    }

    protected override void InteractAlternateWithCounter(object o, EventArgs args) {
        base.InteractAlternateWithCounter(o, args);
        if (playerInRange.HasChildKitchenObject() && HasChildKitchenObject()) {
            bool isPlayerHoldingPlate = playerInRange.GetChildKitchenObject().TryGetComponent<PlateScript>(out PlateScript playerPlateScript);
            bool isCounterHoldingPlate = GetChildKitchenObject().TryGetComponent<PlateScript>(out PlateScript counterPlateScript);
            if (isPlayerHoldingPlate && !isCounterHoldingPlate) {
                KitchenObjectSO tableKitchenObjectSO = GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO();
                bool doesTableHaveBread = tableKitchenObjectSO.naming == FoodItem.Bread;
                bool doesPlayerPlateHaveBurger = playerPlateScript.HasBurger(out BurgerScript playerBurgerScript);
                if (doesTableHaveBread && !doesPlayerPlateHaveBurger) {
                    Destroy(TakeChildKitchenObject().gameObject);
                    Transform burger = Instantiate(playerPlateScript.BurgerSO.prefab).transform;
                    burger.GetComponent<BurgerScript>().TryAddIngredient(tableKitchenObjectSO); // tableKitchenObjectSO IS bread in this case
                    playerPlateScript.SetChildKitchenObject(burger);
                }
                else if (doesPlayerPlateHaveBurger) {
                    KitchenObjectSO ingredient = GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO();
                    if (playerBurgerScript.TryAddIngredient(ingredient)) {
                        Destroy(TakeChildKitchenObject().gameObject);
                    }

                }
                else {
                    playerPlateScript.SetChildKitchenObject(TakeChildKitchenObject());
                }

            }
            else if (!isPlayerHoldingPlate && isCounterHoldingPlate) {
                KitchenObjectSO playerKitchenObject = playerInRange.GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO();
                bool doesPlayerHaveBread = playerInRange.GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO().naming == FoodItem.Bread;
                bool doesCounterPlateHaveBurger = counterPlateScript.HasBurger(out BurgerScript counterBurgerScript);
                if (doesPlayerHaveBread && !doesCounterPlateHaveBurger) {
                    Destroy(playerInRange.TakeChildKitchenObject().gameObject);
                    Transform burger = Instantiate(counterPlateScript.BurgerSO.prefab).transform;
                    burger.GetComponent<BurgerScript>().TryAddIngredient(playerKitchenObject); // playerKitchenObject IS bread in this case
                    counterPlateScript.SetChildKitchenObject(burger);
                }
                else if (doesCounterPlateHaveBurger) {
                    KitchenObjectSO ingredient = playerInRange.GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO();
                    if (counterBurgerScript.TryAddIngredient(ingredient)) {
                        Destroy(playerInRange.TakeChildKitchenObject().gameObject);
                    }
                }
                else {
                    counterPlateScript.SetChildKitchenObject(playerInRange.TakeChildKitchenObject());
                }
            }
        }
    }

}
