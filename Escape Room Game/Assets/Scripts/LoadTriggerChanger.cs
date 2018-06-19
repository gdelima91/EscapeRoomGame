using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class LoadTriggerChanger : MonoBehaviour {

    [Header("Load Triggers to Activate / Deactivate")]
    public GameObject[] loadTriggersToActivate;
    public GameObject[] loadTriggersToDeactivate;

    private void Start() {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<FPSRay>() != null) {
            Activate();
            Deactivate();
        }
    }

    void Activate () {
        if (loadTriggersToActivate.Length > 0) {
            foreach (GameObject go in loadTriggersToActivate) {
                go.SetActive(true);
            }
        }
    }

    void Deactivate () {
        if (loadTriggersToDeactivate.Length > 0) {
            foreach (GameObject go in loadTriggersToDeactivate) {
                go.SetActive(false);
            }
        }
    }
}
