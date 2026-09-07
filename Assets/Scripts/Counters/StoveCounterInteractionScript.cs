using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterInteractionScript : CounterInteractionScript {
    [SerializeField] private KitchenObjectSO kitchenObjectUncooked;
    [SerializeField] private KitchenObjectSO kitchenObjectCooked;
    [SerializeField] private KitchenObjectSO kitchenObjectBurned;
    [SerializeField] private float cookingTimeInSec = 5f;
    [SerializeField] private float burningTimeInSec = 3f;
    private Coroutine cookingCoroutine = null;
    private StoveCounterController stoveCounterController;
    private StoveCounterEventBus stoveCounterEventBus;
    protected override void Awake() {
        base.Awake();
        stoveCounterController = GetComponent<StoveCounterController>();
        stoveCounterEventBus = GetComponent<StoveCounterEventBus>();
    }
    protected override void Start() {
        base.Start();
        stoveCounterEventBus.OnStoveInterrupted += InterruptCooking;
    }
    protected override void InteractWithCounter(object o, EventArgs args) {
        if (playerInRange.HasChildKitchenObject() && !HasChildKitchenObject()) {
            if (playerInRange.GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO() == kitchenObjectUncooked) {
                SetChildKitchenObject(playerInRange.TakeChildKitchenObject());
                cookingCoroutine = StartCoroutine(CookKitchenObject());
            }
        }
        else if (!playerInRange.HasChildKitchenObject() && HasChildKitchenObject()) {
            playerInRange.SetChildKitchenObject(TakeChildKitchenObject());
            stoveCounterEventBus.InvokeOnStoveInterrupted();
        }
    }
    protected override void InteractAlternateWithCounter(object o, EventArgs args) {

        if (HasChildKitchenObject() && playerInRange.HasChildKitchenObject()) {
            bool isPlayerHoldingPlate = playerInRange.GetChildKitchenObject().TryGetComponent<PlateScript>(out PlateScript playerPlateScript);
            if (isPlayerHoldingPlate) {
                if (playerPlateScript.HasBurger(out BurgerScript burgerScript)) {
                    KitchenObjectSO ingredient = GetChildKitchenObject().GetComponent<KitchenObject>().GetKitchenObjectSO();
                    if (burgerScript.TryAddIngredient(ingredient)) {
                        Destroy(TakeChildKitchenObject().gameObject);
                        stoveCounterEventBus.InvokeOnStoveInterrupted();
                    }
                }
            }

        }

    }
    private IEnumerator CookKitchenObject() {
        float cookingProgress = 0f;
        float burningProgress = 0f;
        stoveCounterEventBus.InvokeOnCookingStarted();
        while (cookingProgress < cookingTimeInSec) {
            cookingProgress += Time.deltaTime;
            stoveCounterController.cookingProgressNormalized = cookingProgress / cookingTimeInSec;
            yield return null;
        }
        ClearChildKitchenObjects();
        SetChildKitchenObject(Instantiate(kitchenObjectCooked.prefab).transform);
        stoveCounterEventBus.InvokeOnCookingFinished();
        Debug.Log(kitchenObjectCooked.name + "Is Cooked!");
        stoveCounterEventBus.InvokeOnBurningStarted();
        while (burningProgress < burningTimeInSec) {
            burningProgress += Time.deltaTime;
            stoveCounterController.burningProgressNormalized = burningProgress / burningTimeInSec;
            yield return null;
        }
        ClearChildKitchenObjects();
        SetChildKitchenObject(Instantiate(kitchenObjectBurned.prefab).transform);
        stoveCounterEventBus.InvokeOnBurningFinished();
        Debug.Log(kitchenObjectBurned.name + "Is Burned!");
        cookingCoroutine = null;
    }
    private void InterruptCooking(object o, EventArgs args) {
        if (cookingCoroutine != null) {
            stoveCounterController.cookingProgressNormalized = 0f;
            stoveCounterController.burningProgressNormalized = 0f;
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
        }
    }
}
