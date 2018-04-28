using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int maxHP;

    private int currentHP;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage (int damage) {
        currentHP = currentHP - damage;

        if (currentHP <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
