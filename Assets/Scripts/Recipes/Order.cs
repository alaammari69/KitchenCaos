using System;

[Serializable]
public class Order {
    public BurgerRecipeSO burgerRecipeSO;
    private float patienceInSec;
    public float PatienceInSec => patienceInSec;
    public float currentPatienceInSec;
    public Order(BurgerRecipeSO burgerRecipeSO, float patienceInSec) {
        this.burgerRecipeSO = burgerRecipeSO;
        this.patienceInSec = patienceInSec;
        this.currentPatienceInSec = patienceInSec;
    }
}