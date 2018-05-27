using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour {
    public static CustomSceneManager Instance { set; get; }

    public GameObject player;
    public GameObject ui;
    public LoadTriggerEnums.Levels[] levelsToUnload;
    public LoadTriggerEnums.Levels[] levelsToLoad;

    private Transform levelsTransform;
    private GameObject[] levels;

    private void Awake() {
        Instance = this;

        // Activate Player
        if (player != null && !player.activeSelf) {
            player.SetActive(true);
        }

        // Activate UI
        if (ui != null && !ui.activeSelf) {
            ui.SetActive(true);
        }

        // Find and Set levels
        if (GameObject.Find("Levels") != null) {
            levelsTransform = GameObject.Find("Levels").transform;

            levels = new GameObject[levelsTransform.childCount];

            // Set levels
            for (int i = 0; i < levelsTransform.childCount; i++) {
                levels[i] = levelsTransform.GetChild(i).gameObject;
            }

            // Unload Levels
            foreach (LoadTriggerEnums.Levels level in levelsToUnload) {
                UnloadLevel(levels[(int)level - 1]);
            }

            // Load Levels
            foreach (LoadTriggerEnums.Levels level in levelsToLoad) {
                LoadLevel(levels[(int)level - 1]);
            }
        }
    }

    public void LoadLevel (GameObject go) {
        if (!go.activeSelf && go!=null) {
            go.SetActive(true);
        }
    }

    public void UnloadLevel (GameObject go) {
        if (go.activeSelf && go != null) {
            go.SetActive(false);
        }
    }
}
