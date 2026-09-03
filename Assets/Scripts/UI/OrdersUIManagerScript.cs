using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersUIManagerScript : MonoBehaviour {
    [SerializeField] private RecipeManagerController recipeManagerController;
    [SerializeField] private GameObject orderTemplate;
    void Start() {
        recipeManagerController.RecipeManagerEventBus.OnOrderAddedToQueue += AddOrderToUI;
        recipeManagerController.RecipeManagerEventBus.OnOrderRemovedFromQueue += RemoveOrderFromUI;
    }
    void OnDisable() {
        recipeManagerController.RecipeManagerEventBus.OnOrderAddedToQueue -= AddOrderToUI;
        recipeManagerController.RecipeManagerEventBus.OnOrderRemovedFromQueue -= RemoveOrderFromUI;
    }
    public void AddOrderToUI(object o, Order order) {
        GameObject newOrderUI = Instantiate(orderTemplate, transform);
        newOrderUI.GetComponent<OrderTemplateScript>().SetOrderRecipe(order);
        newOrderUI.SetActive(true);
    }
    public void RemoveOrderFromUI(object o, Order order) {
        foreach (Transform orderUI in transform) {
            if (orderUI.GetComponent<OrderTemplateScript>().Order == order) {
                Destroy(orderUI.gameObject);
                return;
            }
        }
    }
}
