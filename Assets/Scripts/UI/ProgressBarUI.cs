using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private Image bar;
    [SerializeField] private Color cookingColor;
    [SerializeField] private Color burningColor;
    private StoveCounterController stoveCounterController;
    private StoveCounterEventBus stoveCounterEventBus;
    private const string COOKING_STAGE = "cooking_stage";
    private const string BURNING_STAGE = "burning_stage";
    private string currentStage;
    // Start is called before the first frame update
    void Awake() {
        stoveCounterController = GetComponentInParent<StoveCounterController>();
        stoveCounterEventBus = GetComponentInParent<StoveCounterEventBus>();
    }
    void OnEnable() {
        stoveCounterEventBus.OnCookingStarted += EnableProgressBar;

        stoveCounterEventBus.OnBurningStarted += ChangeToBurningColor;

        stoveCounterEventBus.OnStoveInterrupted += DisableProgressBar;
        stoveCounterEventBus.OnBurningFinished += DisableProgressBar;
    }
    void Start() {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        switch (currentStage) {
            case COOKING_STAGE:
                Debug.Log(stoveCounterController.cookingProgressNormalized);
                bar.fillAmount = stoveCounterController.cookingProgressNormalized;
                break;
            case BURNING_STAGE:
                Debug.Log(stoveCounterController.burningProgressNormalized);
                bar.fillAmount = stoveCounterController.burningProgressNormalized;
                break;
        }
    }
    private void EnableProgressBar(object o, EventArgs args) {
        gameObject.SetActive(true);
        bar.fillAmount = 0;
        bar.color = cookingColor;
        currentStage = COOKING_STAGE;
        Debug.Log(currentStage);
    }
    private void DisableProgressBar(object o, EventArgs args) {
        gameObject.SetActive(false);
        currentStage = null;
    }
    private void ChangeToBurningColor(object o, EventArgs args) {
        bar.color = burningColor;
        bar.fillAmount = 0;
        currentStage = BURNING_STAGE;
        Debug.Log(currentStage);
    }
}
