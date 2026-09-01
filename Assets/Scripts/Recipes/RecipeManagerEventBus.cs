using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagerEventBus : MonoBehaviour {
    public event EventHandler<BurgerRecipeSO> OnRecipeAddedToQueue;
    public void InvokeOnRecipeAddedToQueue(BurgerRecipeSO burgerRecipeSO) {
        OnRecipeAddedToQueue?.Invoke(this, burgerRecipeSO);
    }
    public event EventHandler<BurgerRecipeSO> OnRecipeRemovedFromQueue;
    public void InvokeOnRecipeRemovedFromQueue(BurgerRecipeSO burgerRecipeSO) {
        OnRecipeRemovedFromQueue?.Invoke(this, burgerRecipeSO);
    }
}
