using UnityEngine;

[CreateAssetMenu(fileName = "Slicing Recipe", menuName = "Scriptable Objects/Slicing Recipe")]
public class SlicingRecipeSO : ScriptableObject {
    public KitchenObjectSO input;
    public KitchenObjectSO output;
}
