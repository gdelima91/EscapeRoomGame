using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class KillVolume : MonoBehaviour {

    public bool b_Active = false;
    public AudioClip electricitySFX;

    private Collider _collider;
    private GameObject[] lightning;
    private AudioSource audioSource;
    private Electricity electricity;

	// Use this for initialization
	void Start () {
        _collider = GetComponent<Collider>();
        audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.playOnAwake = true;
        //audioSource.Stop();
        electricity = GetComponent<Electricity>();

        _collider.isTrigger = true;

        lightning = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++) {
            lightning[i] = transform.GetChild(i).gameObject;
        }

        SetLightning(b_Active);
    }

    private void OnTriggerEnter(Collider other) {
        if (b_Active) {
            if (other.GetComponent<PlayerHealth>() != null) {
                other.GetComponent<PlayerHealth>().TakeDamage(other.GetComponent<PlayerHealth>().maxHP);
            }
        }
    }

    public void Set_B_Active (bool b) {
        b_Active = b;
        SetLightning(b_Active);

        if (b_Active) {
            audioSource.Play();
            print(audioSource.isPlaying);
        }
        else {
            audioSource.Stop();
            print(audioSource.isPlaying);
        }
    }

    void SetLightning (bool b) {
        foreach (GameObject lightningGO in lightning) {
            lightningGO.SetActive(b);
        }
    }
}
