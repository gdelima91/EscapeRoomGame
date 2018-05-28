using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public CheckPoint checkPoint;
    public int maxHP;

    private int currentHP;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
	}

    void Update( )
    {
        if( Input.GetKeyDown(KeyCode.L) )
        {
            TakeDamage( 1000 );
        }
    }

    public void TakeDamage (int damage) {
        currentHP = currentHP - damage;

        if (currentHP <= 0) {
            transform.position = checkPoint.ResetPlayer( );
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
