using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterInteractionScript : CounterInteractionScript {
    [SerializeField] private RecipeManagerController recipeManagerController;
    protected override void Awake() {
        base.Awake();
    }
    protected override void OnEnable() {
        base.OnEnable();
    }
    protected override void OnDisable() {
        base.OnDisable();
    }
    protected override void InteractWithCounter(object o, EventArgs args) {
        base.InteractWithCounter(o, args);
        if (playerInRange.HasChildKitchenObject()) {
            if (playerInRange.GetChildKitchenObject().TryGetComponent<PlateScript>(out PlateScript playerPlate)) {
                GameObject plate = playerInRange.TakeChildKitchenObject().gameObject;
                if (playerPlate.HasBurger(out BurgerScript burgerScript)) {
                    recipeManagerController.TrySendingOrder(burgerScript);
                }
                Destroy(plate);
            }
        }

    }
    protected override void InteractAlternateWithCounter(object o, EventArgs args) {
        base.InteractAlternateWithCounter(o, args);
    }

}
