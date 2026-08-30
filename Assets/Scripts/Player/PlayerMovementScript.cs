using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {
    private PlayerEventBus playerEventBus;
    [SerializeField] private float movingSpeed = 6f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private float bodyRadius = 0.6f;
    private float bodyOffset = 0.65f;

    private Vector2 movementDirection = Vector2.zero;

    void Awake() {
        playerEventBus = GetComponent<PlayerEventBus>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void FixedUpdate() {

    }



    private bool wasPlayerMoving = false;

    /// <summary>
    /// This function moves the player according to the input vector given
    /// only call this in FixedUpdate
    /// </summary>
    /// <param name="input"> input vector 2d</param>
    /// 
    public void MoveTowards(Vector2 input) {
        // checking if theres no input in this frame, was the player stationary before or he just stoped moving this frame
        if (input == Vector2.zero) {
            if (wasPlayerMoving) {
                playerEventBus.InvokeOnPlayerStopedMoving();
                wasPlayerMoving = false;
            }
            return;
        }
        Vector3 movementDirection = new Vector3(input.x, 0f, input.y);
        if (!wasPlayerMoving) {
            playerEventBus.InvokeOnPlayerStartedMoving();
            wasPlayerMoving = true;
        }

        // test if player is allowed to move
        if (CanMove(movementDirection)) {
            // update position
            transform.Translate(movementDirection * Time.fixedDeltaTime * movingSpeed);
        }
        else // if the player can't move check if he can move diagonally 
        {
            if (CanMove(new Vector3(movementDirection.x, 0f, 0f))) {
                transform.Translate((new Vector3(movementDirection.x, 0f, 0f)) * Time.fixedDeltaTime * movingSpeed);
            }
            else if (CanMove(new Vector3(0f, 0f, movementDirection.z))) {
                transform.Translate((new Vector3(0f, 0f, movementDirection.z)) * Time.fixedDeltaTime * movingSpeed);
            }
        }

        //update visual rotation
        playerVisual.transform.rotation = Quaternion.Slerp(playerVisual.transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.fixedDeltaTime);

    }

    /// <summary>
    /// This function tests if the player can move if there's no collisions detected
    /// </summary>
    /// <param name="movementDirection"> direction which the player wants to move tewards </param>
    /// <returns></returns>
    private bool CanMove(Vector3 movementDirection) {
        RaycastHit hit;
        bool canMove = !Physics.SphereCast(transform.position + Vector3.up * bodyOffset, bodyRadius, movementDirection.normalized, out hit, movingSpeed * Time.fixedDeltaTime);
        return canMove;
    }

    void OnDrawGizmos() {
        GizmoDrawer.DrawSphere(transform.position + Vector3.up * bodyOffset, bodyRadius, Color.red);
    }


}
