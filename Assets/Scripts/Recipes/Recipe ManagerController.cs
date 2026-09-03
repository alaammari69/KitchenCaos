using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagerController : MonoBehaviour {
    [SerializeField] private List<BurgerRecipeSO> possibleRecipes;
    [SerializeField] private int maxNbrRecipesOnQueue = 3;
    [SerializeField] private int delayBetweenRecipesInSec = 4;
    [SerializeField] private float maxOrderPatienceInSec = 20f;
    [SerializeField] private float minOrderPatienceInSec = 10f;
    private List<Order> ordersOnQueue;
    public List<Order> OrdersOnQueue => ordersOnQueue;
    private RecipeManagerEventBus recipeManagerEventBus;
    public RecipeManagerEventBus RecipeManagerEventBus => recipeManagerEventBus;
    private Coroutine currentCoroutine = null;
    [SerializeField] private bool isRecievingNewOrders = true;
    public bool IsRecievingNewOrders => isRecievingNewOrders;
    public void RecieveNewOrders(bool val) {
        isRecievingNewOrders = val;
    }
    void Awake() {
        recipeManagerEventBus = GetComponent<RecipeManagerEventBus>();
        ordersOnQueue = new List<Order>();
        ordersToRemove = new List<Order>();
    }
    void Update() {
        UpdatePatienceValues();

        if (isRecievingNewOrders && ordersOnQueue.Count < maxNbrRecipesOnQueue && (currentCoroutine == null)) {
            currentCoroutine = StartCoroutine(TryAddOrderToQueue());
        }
    }
    private List<Order> ordersToRemove;
    private void UpdatePatienceValues() {
        foreach (Order order in ordersOnQueue) {
            order.currentPatienceInSec -= Time.deltaTime;
            if (order.currentPatienceInSec <= 0) {
                ordersToRemove.Add(order);
                recipeManagerEventBus.InvokeOnOrderMissed();
            }
        }
        ordersToRemove.ForEach((o) => TryRemoveOrderFromQueue(o.burgerRecipeSO));
        ordersToRemove.Clear();
    }
    private IEnumerator TryAddOrderToQueue() {
        yield return new WaitForSeconds(delayBetweenRecipesInSec);
        int randomIndex = Random.Range(0, possibleRecipes.Count);
        BurgerRecipeSO chosenRecipe = possibleRecipes[randomIndex];
        Order newOrder = new Order(chosenRecipe, Random.Range(minOrderPatienceInSec, maxOrderPatienceInSec));
        ordersOnQueue.Add(newOrder);
        recipeManagerEventBus.InvokeOnOrderAddedToQueue(newOrder);
        currentCoroutine = null;
    }
    private bool TryRemoveOrderFromQueue(BurgerRecipeSO burgerRecipeSO) {
        Order order = ordersOnQueue.Find((o) => (o.burgerRecipeSO == burgerRecipeSO));
        if (order != null) {
            recipeManagerEventBus.InvokeOnOrderRemovedFromQueue(order);
            ordersOnQueue.Remove(order);
            return true;
        }
        return false;

    }
    private bool TryRemoveOrderFromQueue(BurgerScript burgerScript) {
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
                if (allMatch) return TryRemoveOrderFromQueue(possibleRecipe);
            }
            else {
                continue;
            }
        }
        return false;
    }
    public bool TrySendingOrder(BurgerScript burgerScript) {
        bool isBurgerRecipeCorrect = TryRemoveOrderFromQueue(burgerScript);
        if (!isBurgerRecipeCorrect) recipeManagerEventBus.InvokeOnWrongOrderDelivered();
        return isBurgerRecipeCorrect;
    }
}
