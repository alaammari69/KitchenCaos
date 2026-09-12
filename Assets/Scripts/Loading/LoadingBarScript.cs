using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarScript : MonoBehaviour {
    [SerializeField] private Image progressBar;

    void LateUpdate() {
        UpdateProgressBar();
    }
    private void UpdateProgressBar() {
        progressBar.fillAmount = SceneLoader.CurrentLoadingProgress;
    }
}
