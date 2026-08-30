using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerScript : KitchenObject {
    [SerializeField] private List<KitchenObjectSO> allowedIngredient;
    private List<KitchenObjectSO> currentIngredients = new List<KitchenObjectSO>();
    public List<KitchenObjectSO> GetAddedIngredients => currentIngredients;
    public event EventHandler<KitchenObjectSO> OnIngredientAdded;
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!currentIngredients.Contains(kitchenObjectSO) && allowedIngredient.Contains(kitchenObjectSO)) {
            currentIngredients.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, kitchenObjectSO);
            return true;
        }
        return false;
    }

}
