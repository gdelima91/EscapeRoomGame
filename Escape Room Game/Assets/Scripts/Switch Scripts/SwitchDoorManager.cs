using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoorManager : MonoBehaviour
{
    public bool isAllLeversTurnedOn = false;
    public Lever[] levers;

    public Animator animator;
    public RuntimeAnimatorController openAnim;
    public RuntimeAnimatorController closeAnim;

    void Update( )
    {
        for( int i = 0; i < levers.Length; i++ )
        {
            if( levers[i].isTurnedOn )
            {
                isAllLeversTurnedOn = true;
            }
            else
            {
                isAllLeversTurnedOn = false;
                break;
            }
        }
        if( isAllLeversTurnedOn )
        {
            animator.runtimeAnimatorController = openAnim;
        }
        else
        {            
            animator.runtimeAnimatorController = closeAnim;
        }
    }
}
