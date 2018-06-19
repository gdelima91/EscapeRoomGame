using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour {

    public GameObject winText;

	// Use this for initialization
	void Start () {
		if (winText != null) {
            winText.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<FPSRay>() != null) {
            winText.SetActive(true);
        }
    }
}
