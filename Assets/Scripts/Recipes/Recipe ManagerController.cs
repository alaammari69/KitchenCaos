using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagerController : MonoBehaviour {
    [SerializeField] private List<BurgerRecipeSO> possibleRecipes;
    [SerializeField] private int maxNbrRecipesOnQueue = 3;
    [SerializeField] private int delayBetweenRecipesInSec = 4;
    private List<BurgerRecipeSO> recipesOnQueue;
    public List<BurgerRecipeSO> RecipesOnQueue => recipesOnQueue;
    private RecipeManagerEventBus recipeManagerEventBus;
    public RecipeManagerEventBus RecipeManagerEventBus => recipeManagerEventBus;
    private Coroutine currentCoroutine = null;
    private bool isRecievingNewOrders = true;
    public bool IsRecievingNewOrders => isRecievingNewOrders;
    public void RecieveNewOrders(bool val) {
        isRecievingNewOrders = val;
    }
    void Awake() {
        recipeManagerEventBus = GetComponent<RecipeManagerEventBus>();
        recipesOnQueue = new List<BurgerRecipeSO>();
    }
    void Update() {
        if (isRecievingNewOrders && recipesOnQueue.Count < maxNbrRecipesOnQueue && (currentCoroutine == null)) {
            currentCoroutine = StartCoroutine(TryAddRecipeToQueue());
        }
    }
    private IEnumerator TryAddRecipeToQueue() {
        yield return new WaitForSeconds(delayBetweenRecipesInSec);
        int randomIndex = Random.Range(0, possibleRecipes.Count);
        BurgerRecipeSO chosenRecipe = possibleRecipes[randomIndex];
        recipesOnQueue.Add(chosenRecipe);
        recipeManagerEventBus.InvokeOnRecipeAddedToQueue(chosenRecipe);
        currentCoroutine = null;
    }
    private bool TryRemoveRecipeFromQueue(BurgerRecipeSO burgerRecipeSO) {
        if (recipesOnQueue.Remove(burgerRecipeSO)) {
            recipeManagerEventBus.InvokeOnRecipeRemovedFromQueue(burgerRecipeSO);
            return true;
        }
        return false;

    }
    private bool TryRemoveRecipeFromQueue(BurgerScript burgerScript) {
        bool allMatch;
        foreach (BurgerRecipeSO possibleRecipe in possibleRecipes) {
            allMatch = true;
            if (possibleRecipe.ingredients.Count == burgerScript.GetAddedIngredients.Count) {
                foreach (KitchenObjectSO ingredient in possibleRecipe.ingredients) {
                    if (!burgerScript.GetAddedIngredients.Contains(ingredient)) {
                        allMatch = false;
                        break;
                    }
                }
                if (allMatch) return TryRemoveRecipeFromQueue(possibleRecipe);
            }
            else {
                continue;
            }
        }
        return false;
    }
    public bool TrySendingOrder(BurgerScript burgerScript) {
        bool isBurgerRecipeCorrect = TryRemoveRecipeFromQueue(burgerScript);
        if (!isBurgerRecipeCorrect) recipeManagerEventBus.InvokeOnWrongOrderDelivered();
        return isBurgerRecipeCorrect;
    }
}
