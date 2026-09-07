using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterInteractionScript : CounterInteractionScript {
    [SerializeField] private RecipeManagerController recipeManagerController;
    private DeliveryCounterEventBus deliveryCounterEventBus;
    protected override void Awake() {
        base.Awake();
        deliveryCounterEventBus = counterEventBus as DeliveryCounterEventBus;
    }
    protected override void InteractWithCounter(object o, EventArgs args) {
        base.InteractWithCounter(o, args);
        if (playerInRange.HasChildKitchenObject()) {
            if (playerInRange.GetChildKitchenObject().TryGetComponent<PlateScript>(out PlateScript playerPlate)) {
                GameObject plate = playerInRange.TakeChildKitchenObject().gameObject;
                if (playerPlate.HasBurger(out BurgerScript burgerScript)) {
                    bool isCorrect = recipeManagerController.TrySendingOrder(burgerScript);
                    if (isCorrect) {
                        deliveryCounterEventBus.InvokeOnSuccessOrder();
                        Destroy(plate);
                        return;
                    }
                }
                deliveryCounterEventBus.InvokeOnFailOrder();
                Destroy(plate);
            }
        }

    }
    protected override void InteractAlternateWithCounter(object o, EventArgs args) {
        base.InteractAlternateWithCounter(o, args);
    }

}
