using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderTemplateScript : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private IngredientsIconsScript ingredientsIconsScript;
    private BurgerRecipeSO burgerRecipeSO;
    public BurgerRecipeSO BurgerRecipeSO => burgerRecipeSO;
    public void SetOrderRecipe(BurgerRecipeSO burgerRecipeSO) {
        this.burgerRecipeSO = burgerRecipeSO;
        recipeName.text = burgerRecipeSO.burgerRecipeName.ToString();
        foreach (KitchenObjectSO ingredient in burgerRecipeSO.ingredients) {
            ingredientsIconsScript.AddIngredientIcon(ingredient.icon);
        }
    }
}
