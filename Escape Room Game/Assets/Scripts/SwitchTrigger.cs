using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class SwitchTrigger : MonoBehaviour {

    public Electricity electricity;

    private BoxCollider _collider;
    private bool doOnce = false;

	// Use this for initialization
	void Start () {
        _collider = GetComponent<BoxCollider>();
	}

    private void OnTriggerEnter(Collider other) {
        if (!doOnce) {
            doOnce = true;
            electricity.isGettingElectricity = false;
        }
    }
}
