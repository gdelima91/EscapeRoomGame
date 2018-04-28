using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class KillVolume : MonoBehaviour {

    public bool b_Active = false;

    private Collider _collider;
    private GameObject lightning;

	// Use this for initialization
	void Start () {
        _collider = GetComponent<Collider>();

        _collider.isTrigger = true;

        if (transform.GetChild(0).gameObject != null)
            lightning = transform.GetChild(0).gameObject;

        lightning.SetActive(b_Active);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerHealth>() != null) {
            other.GetComponent<PlayerHealth>().TakeDamage(other.GetComponent<PlayerHealth>().maxHP);
        }
    }

    public void Set_B_Active (bool b) {
        b_Active = b;
        lightning.SetActive(b_Active);
    }
}
