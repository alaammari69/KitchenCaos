using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour {
    [SerializeField] private float interactionRange_offset_y = 0.6f;
    [SerializeField] private float interactionRange_offset_z = 1f;
    [SerializeField] private float interactionRange_offset_x = 0f;

    [SerializeField] private float interactionRange_width = 0.3f;
    [SerializeField] private float interactionRange_height = 1f;
    [SerializeField] private float interactionRange_depth = 0.5f;

    private CounterEventBus counterEventBus;

    void Awake() {
        counterEventBus = GetComponent<CounterEventBus>();
    }
    void FixedUpdate() {
        CheckNearByPlayer();
    }
    private bool wasPlayerInRange = false;
    private PlayerController playerInRange;
    private void CheckNearByPlayer() {
        Vector3 boxCenter = transform.position + transform.rotation * new Vector3(interactionRange_offset_x, interactionRange_offset_y, interactionRange_offset_z);
        Vector3 halfExtents = new Vector3(interactionRange_width, interactionRange_height, interactionRange_depth) * 0.5f;

        Collider[] hits = Physics.OverlapBox(boxCenter, halfExtents, transform.rotation);
        if (hits.Length == 0) {
            if (wasPlayerInRange) {
                counterEventBus.InvokeOnPlayerOutOfRange(playerInRange);
                playerInRange = null;
                wasPlayerInRange = false;
            }
        }
        else {
            if (!wasPlayerInRange) {
                playerInRange = hits[0].transform.GetComponent<PlayerController>();
                counterEventBus.InvokeOnPlayerInRange(playerInRange);
                wasPlayerInRange = true;
            }
        }
    }

    void OnDrawGizmos() {
        GizmoDrawer.DrawBox(
            transform.position + transform.rotation * new Vector3(interactionRange_offset_x, interactionRange_offset_y, interactionRange_offset_z),
            new Vector3(interactionRange_width, interactionRange_height, interactionRange_depth) * 0.5f,
            transform.rotation,
            Color.red
        );
    }
}
