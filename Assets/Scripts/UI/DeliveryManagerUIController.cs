using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUIController : MonoBehaviour {
    private OrdersUIManagerScript ordersUIManagerScript;
    void Awake() {
        ordersUIManagerScript = GetComponentInChildren<OrdersUIManagerScript>();
    }
}
