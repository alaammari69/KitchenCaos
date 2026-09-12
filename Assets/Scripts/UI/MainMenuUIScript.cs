using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIScript : MonoBehaviour {
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    void Start() {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        startButton.Select();
    }

    private void StartGame() {
        SceneLoader.TransitionToScene(SceneLoader.Scene.GameScene, enableLoadingScreen: true);
    }

    private void QuitGame() {
        Application.Quit();
    }
}
