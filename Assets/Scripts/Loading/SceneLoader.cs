using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {
    private static float currentLoadingProgress = 0f;
    public static float CurrentLoadingProgress => currentLoadingProgress;
    [Serializable]
    public enum Scene {
        GameScene,
        MainMenu,
        LoadingScene
    }
    public static void TransitionToScene(Scene scene, bool enableLoadingScreen) {
        if (enableLoadingScreen) {
            CoroutineRunner.Instance.StartCoroutine(LoadAsynchWithLoadingScreen(scene));
        }
        else {
            CoroutineRunner.Instance.StartCoroutine(LoadAsynch(scene));
        }

    }
    private static IEnumerator LoadAsynchWithLoadingScreen(Scene scene) {
        yield return CoroutineRunner.Instance.StartCoroutine(LoadAsynch(Scene.LoadingScene));

        CoroutineRunner.Instance.StartCoroutine(LoadAsynch(scene));
    }

    private static IEnumerator LoadAsynch(Scene scene) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.ToString());
        if (!operation.isDone) {
            currentLoadingProgress = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
}
