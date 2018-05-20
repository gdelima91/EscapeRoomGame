using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour {
    public static CustomSceneManager Instance { set; get; }

    public string[] levelsToLoad;

    private void Awake() {
        Instance = this;

        Load("Player");
        Load("UI");

        foreach (string level in levelsToLoad) {
            Load(level);
        }
    }

    public void Load (string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void Unload (string sceneName) {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadLevel (GameObject go) {
        if (!go.activeSelf) {
            go.SetActive(true);
        }
    }

    public void UnloadLevel (GameObject go) {
        if (go.activeSelf) {
            go.SetActive(false);
        }
    }
}
