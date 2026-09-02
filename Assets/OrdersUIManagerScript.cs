using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersUIManagerScript : MonoBehaviour {
    [SerializeField] private RecipeManagerController recipeManagerController;
    [SerializeField] private GameObject orderTemplate;
    void Start() {
        recipeManagerController.RecipeManagerEventBus.OnRecipeAddedToQueue += AddOrderToUI;
        recipeManagerController.RecipeManagerEventBus.OnRecipeRemovedFromQueue += RemoveOrderFromUI;
    }
    void OnDisable() {
        recipeManagerController.RecipeManagerEventBus.OnRecipeAddedToQueue -= AddOrderToUI;
        recipeManagerController.RecipeManagerEventBus.OnRecipeRemovedFromQueue -= RemoveOrderFromUI;
    }
    public void AddOrderToUI(object o, BurgerRecipeSO burgerRecipeSO) {
        GameObject newOrderUI = Instantiate(orderTemplate, transform);
        newOrderUI.GetComponent<OrderTemplateScript>().SetOrderRecipe(burgerRecipeSO);
        newOrderUI.SetActive(true);
    }
    public void RemoveOrderFromUI(object o, BurgerRecipeSO burgerRecipeSO) {
        foreach (Transform orderUI in transform) {
            if (orderUI.GetComponent<OrderTemplateScript>().BurgerRecipeSO == burgerRecipeSO) {
                Destroy(orderUI.gameObject);
                return;
            }
        }
    }
}
