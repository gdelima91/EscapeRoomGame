using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour {

    public string[] loadName;
    public string[] unloadName;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<FPSRay>() != null) {
            Load();
            StartCoroutine(Unload());
        }
    }

    void Load () {
        foreach (string name in loadName) {
            CustomSceneManager.Instance.Load(name);
        }
    }

    IEnumerator Unload () {
        yield return new WaitForSeconds(0.1f);
        foreach (string name in unloadName) {
            CustomSceneManager.Instance.Unload(name);
        }
    }
}
