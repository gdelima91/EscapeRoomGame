using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActivated = false;

    void OnTriggerEnter( Collider other )
    {
        if( other.tag == "Player" )
        {
            if( !isActivated )
            {
                isActivated = true;
                other.GetComponent<PlayerHealth>().checkPoint = this;
            }
        }
    }
    public Vector3 ResetPlayer( )
    {
        return transform.position;
    }
}
