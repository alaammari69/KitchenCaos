using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterInteractionScript : CounterInteractionScript {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private int maxPlateNumber = 10;

    private int counter = 0;
    private void InvokeInteractAlternative(object o, EventArgs args) {
        counterEventBus.InvokeOnInteractedAlternate();
    }
    protected override void InteractWithCounter(object o, EventArgs args) {
        if (!playerInRange.HasChildKitchenObject() && HasChildKitchenObject()) {
            playerInRange.SetChildKitchenObject(TakePlate());
            counter--;
        }
    }
    protected override void InteractAlternateWithCounter(object o, EventArgs args) {
        base.InteractAlternateWithCounter(o, args);
        if (counter < maxPlateNumber) {
            Transform newPlate = Instantiate(kitchenObjectSO.prefab).transform;
            Transform topPlate = GetTopPlate();
            if (topPlate != null) {
                topPlate.GetComponent<PlateScript>().SetChildKitchenObject(newPlate);
            }
            else {
                SetChildKitchenObject(newPlate);
            }
            counter++;
        }
    }

    private Transform GetTopPlate() {
        if (HasChildKitchenObject()) {
            Transform topPlate = SearchForTopPlate(GetChildKitchenObject());
            return topPlate;
        }
        else {
            return null;
        }
    }


    private Transform SearchForTopPlate(Transform plate) {
        PlateScript plateScript = plate.GetComponent<PlateScript>();
        if (!plateScript.HasChildKitchenObject()) {
            return plateScript.transform;
        }
        else {
            Transform nextPlate = plateScript.GetChildKitchenObject();
            return SearchForTopPlate(nextPlate);
        }
    }
    private Transform TakePlate() {
        Transform topPlate = GetTopPlate();
        if (topPlate != null) {
            topPlate.SetParent(null);
            return topPlate;
        }
        else {
            return null;
        }
    }


}
