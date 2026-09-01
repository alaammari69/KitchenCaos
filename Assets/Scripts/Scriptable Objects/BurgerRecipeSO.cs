using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Burger Recipe", menuName = "Scriptable Objects/Burger Recipe")]
public class BurgerRecipeSO : ScriptableObject {
    public List<KitchenObjectSO> ingredients;
    public BurgerRecipeName burgerRecipeName;
}
