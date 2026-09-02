using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagerEventBus : MonoBehaviour {
    public event EventHandler<BurgerRecipeSO> OnRecipeAddedToQueue;
    public void InvokeOnRecipeAddedToQueue(BurgerRecipeSO burgerRecipeSO) {
        OnRecipeAddedToQueue?.Invoke(this, burgerRecipeSO);
        Debug.Log("recipe added");
    }
    public event EventHandler<BurgerRecipeSO> OnRecipeRemovedFromQueue;
    public void InvokeOnRecipeRemovedFromQueue(BurgerRecipeSO burgerRecipeSO) {
        OnRecipeRemovedFromQueue?.Invoke(this, burgerRecipeSO);
        Debug.Log("recipe removed");
    }
    public event EventHandler OnWrongOrderDelivered;
    public void InvokeOnWrongOrderDelivered() {
        OnWrongOrderDelivered?.Invoke(this, EventArgs.Empty);
        Debug.Log("wrong recipe");
    }
}
