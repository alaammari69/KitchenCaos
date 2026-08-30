using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen Object", menuName = "Scriptable Objects/Kitchen Object")]
public class KitchenObjectSO : ScriptableObject {
    [SerializeField] public GameObject prefab;
    [SerializeField] public Sprite icon;
    [SerializeField] public FoodItem naming;
}
