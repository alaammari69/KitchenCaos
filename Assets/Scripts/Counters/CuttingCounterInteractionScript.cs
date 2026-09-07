using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterInteractionScript : CounterInteractionScript {
    [SerializeField] private SlicingRecipeSO[] slicingRecipesArray;
    protected override void Start() {
        base.Start();
        counterEventBus.OnKitchenObjectSliced += ChangeToSlicedVariant;
    }
    protected override void OnDisable() {
        base.OnDisable();
        counterEventBus.OnKitchenObjectSliced -= ChangeToSlicedVariant;
    }
    private SlicingRecipeSO currentSlicingRecipe = null;
    protected override void InteractWithCounter(object o, EventArgs args) {
        Debug.Log(playerInRange.transform.name + "IS INTERACTING WITH " + transform.name);

        PlayerController playerController = playerInRange.GetComponent<PlayerController>();

        if (playerController.HasChildKitchenObject()) {
            SlicingRecipeSO slicingRecipe = GetAccordingRecipe(playerController.GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO());
            if (slicingRecipe != null) {
                SetChildKitchenObject(playerController.TakeChildKitchenObject());
                currentSlicingRecipe = slicingRecipe;
                counterEventBus.InvokeOnDropKitchenObject();

            }
        }
        else if (HasChildKitchenObject()) {
            playerController.SetChildKitchenObject(TakeChildKitchenObject());
            currentSlicingRecipe = null;
            counterEventBus.InvokeOnPickupKitchenObject();
        }

    }
    protected override void InteractAlternateWithCounter(object o, EventArgs args) {
        if (currentSlicingRecipe != null) {

            counterEventBus.InvokeOnInteractedAlternate();
            //playerInRange.playerEventBus.OnPlayerInteractAlternate -= InteractAlternateWithCounter;
            return;
        }
        else {
            if (HasChildKitchenObject() && playerInRange.HasChildKitchenObject()) {
                bool isPlayerHoldingPlate = playerInRange.GetChildKitchenObject().TryGetComponent<PlateScript>(out PlateScript playerPlateScript);
                if (isPlayerHoldingPlate) {
                    if (playerPlateScript.HasBurger(out BurgerScript burgerScript)) {
                        KitchenObjectSO ingredient = GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO();
                        if (burgerScript.TryAddIngredient(ingredient)) {
                            Destroy(TakeChildKitchenObject().gameObject);
                        }
                    }
                }

            }
        }
    }
    private void ChangeToSlicedVariant(object o, EventArgs args) {
        ClearChildKitchenObjects();
        GameObject kitchenObject = Instantiate(currentSlicingRecipe.output.prefab);
        SetChildKitchenObject(kitchenObject.transform);
        currentSlicingRecipe = null;
    }

    private SlicingRecipeSO GetAccordingRecipe(KitchenObjectSO kitchenObject) {
        foreach (SlicingRecipeSO slicingRecipe in slicingRecipesArray) {
            if (slicingRecipe.input == kitchenObject) {
                return slicingRecipe;
            }
        }
        return null;
    }
}
