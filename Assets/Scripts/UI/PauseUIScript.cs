using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIScript : MonoBehaviour {
    [SerializeField] private InputManagerScript inputManagerScript;
    [SerializeField] private Button resume;
    private bool isGamePaused = false;
    void Start() {
        inputManagerScript.OnPausePerformed += TogglePauseGame;
        gameObject.SetActive(false);
    }
    private void TogglePauseGame(object o, EventArgs args) {
        if (isGamePaused) {
            ResumeGame();
        }
        else {
            PauseGame();
        }
    }
    public void ReturnToMainMenu() {
        SceneLoader.TransitionToScene(SceneLoader.Scene.MainMenu, enableLoadingScreen: true);
    }
    public void PauseGame() {
        gameObject.SetActive(true);
        isGamePaused = true;
        Time.timeScale = 0f;
        resume.Select();
    }
    public void ResumeGame() {
        gameObject.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1f;
    }
    public void RestartGame() {
        Time.timeScale = 1f;
        SceneLoader.TransitionToScene(SceneLoader.Scene.GameScene, enableLoadingScreen: true);
    }
}
