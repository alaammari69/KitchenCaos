using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerVisualScript : MonoBehaviour {
    [SerializeField] private GameObject bread;
    [SerializeField] private GameObject meatPattyUncooked;
    [SerializeField] private GameObject meatPattyCooked;
    [SerializeField] private GameObject meatPattyBurned;
    [SerializeField] private GameObject cabbageSliced;
    [SerializeField] private GameObject tomatoSliced;
    [SerializeField] private GameObject cheeseBlockSliced;
    private BurgerScript burgerScript;
    void Awake() {
        burgerScript = GetComponentInParent<BurgerScript>();
    }
    void OnEnable() {
        burgerScript.OnIngredientAdded += UpdateBurgerVisual;
    }

    private void UpdateBurgerVisual(object o, KitchenObjectSO kitchenObjectSO) {
        switch (kitchenObjectSO.naming) {
            case FoodItem.Bread:
                bread.SetActive(true);
                break;
            case FoodItem.MeatPattyCooked:
                meatPattyCooked.SetActive(true);
                break;
            case FoodItem.MeatPattyUncooked:
                meatPattyUncooked.SetActive(true);
                break;
            case FoodItem.MeatPattyBurned:
                meatPattyBurned.SetActive(true);
                break;
            case FoodItem.CabbageSliced:
                cabbageSliced.SetActive(true);
                break;
            case FoodItem.TomatoSliced:
                tomatoSliced.SetActive(true);
                break;
            case FoodItem.CheeseBlockSliced:
                cheeseBlockSliced.SetActive(true);
                break;
        }
    }
}
