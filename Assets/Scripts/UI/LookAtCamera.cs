using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    private enum LookAtMode {
        LookAt,
        CameraForward
    }
    [SerializeField] private LookAtMode mode = LookAtMode.CameraForward;
    void LateUpdate() {
        switch (mode) {
            case LookAtMode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case LookAtMode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
        }
    }
}
