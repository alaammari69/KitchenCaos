using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsIconsUIScript : MonoBehaviour {
    [SerializeField] private GameObject iconTemplate;
    private BurgerScript burgerScript;
    void Awake() {
        burgerScript = GetComponentInParent<BurgerScript>();
    }
    void OnEnable() {
        burgerScript.OnIngredientAdded += AddIngredientIcon;
    }
    private void AddIngredientIcon(object o, KitchenObjectSO kitchenObjectSO) {
        GameObject newIconTemplate = Instantiate(iconTemplate, transform);
        newIconTemplate.GetComponent<IconScript>().SetIconImage(kitchenObjectSO.icon);
        newIconTemplate.SetActive(true);
    }

}
