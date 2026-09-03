using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceBarScript : MonoBehaviour {
    [SerializeField] private Gradient gradient;
    private Image patienceBar;
    void Awake() {
        patienceBar = GetComponent<Image>();
    }
    public void SetPatienceBarValue(float val) {
        patienceBar.fillAmount = val;
        patienceBar.color = gradient.Evaluate(val);
    }
}
