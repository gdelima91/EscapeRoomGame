using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadTrigger : MonoBehaviour {

    public LoadTriggerEnums.Levels[] loadLevels;
    public LoadTriggerEnums.Levels[] unloadLevels;

    private Transform levelsTransform;
    private GameObject[] levels;

    private void Start() {
        if (GameObject.Find("Levels") != null) {
            levelsTransform = GameObject.Find("Levels").transform;

            levels = new GameObject[levelsTransform.childCount];

            for (int i = 0; i < levelsTransform.childCount; i++) {
                levels[i] = levelsTransform.GetChild(i).gameObject;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<FPSRay>() != null) {
            Load();
            StartCoroutine(Unload());
        }
    }

    void Load () {
        foreach (LoadTriggerEnums.Levels level in loadLevels) {
            CustomSceneManager.Instance.LoadLevel(levels[(int)level - 1]);
        }
    }

    IEnumerator Unload () {
        yield return new WaitForSeconds(0.1f);
        foreach (LoadTriggerEnums.Levels level in unloadLevels) {
            CustomSceneManager.Instance.UnloadLevel(levels[(int)level - 1]);
        }
    }
}
