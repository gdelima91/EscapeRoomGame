using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour {
    public static CustomSceneManager Instance { set; get; }

    private void Awake() {
        Instance = this;

        Load("Player");
        Load("UI");
        Load("1");
        Load("1a");
    }

    public void Load (string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void Unload (string sceneName) {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
    }
}
