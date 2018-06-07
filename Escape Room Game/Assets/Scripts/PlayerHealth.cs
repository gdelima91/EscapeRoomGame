using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public CheckPoint checkPoint;
    public float maxHP;

    public float currentHP;

    public static PlayerHealth instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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

    public void TakeDamage (float damage) {
        currentHP = currentHP - damage;

        if (currentHP <= 0) {
            transform.position = checkPoint.ResetPlayer( );
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            currentHP = maxHP;
        }

    }
}
