using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour {

    public GameObject[] loadName;
    public GameObject[] unloadName;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<FPSRay>() != null) {
            Load();
            StartCoroutine(Unload());
        }
    }

    void Load () {
        foreach (GameObject go in loadName) {
            CustomSceneManager.Instance.LoadLevel(go);
        }
    }

    IEnumerator Unload () {
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject go in unloadName) {
            CustomSceneManager.Instance.UnloadLevel(go);
        }
    }
}
