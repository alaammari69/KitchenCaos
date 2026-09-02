using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientsIconsScript : MonoBehaviour {
    [SerializeField] private GameObject ingredientIconTemplate;

    public void AddIngredientIcon(Sprite iconSprite) {
        GameObject newIconTemp = Instantiate(ingredientIconTemplate, transform);
        newIconTemp.GetComponent<Image>().sprite = iconSprite;
        newIconTemp.SetActive(true);
    }
}
