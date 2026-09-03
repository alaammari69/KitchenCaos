using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderTemplateScript : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private IngredientsIconsScript ingredientsIconsScript;
    private PatienceBarScript patienceBarScript;
    private Order order;
    public Order Order => order;

    void Awake() {
        patienceBarScript = GetComponentInChildren<PatienceBarScript>();
    }
    public void SetOrderRecipe(Order order) {
        this.order = order;
        recipeName.text = System.Text.RegularExpressions.Regex.Replace(order.burgerRecipeSO.burgerRecipeName.ToString(), @"(?<!^)([A-Z])", " $1");
        foreach (KitchenObjectSO ingredient in order.burgerRecipeSO.ingredients) {
            ingredientsIconsScript.AddIngredientIcon(ingredient.icon);
        }
    }
    void Update() {
        if (order != null) {
            patienceBarScript.SetPatienceBarValue(order.currentPatienceInSec / order.PatienceInSec);
        }
    }
}
