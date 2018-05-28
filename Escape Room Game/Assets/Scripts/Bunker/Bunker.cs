using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public bool isOpenBunker = true;
    public Animator animator;
    public RuntimeAnimatorController openBunker;
    public RuntimeAnimatorController closeBunker;

    void Start( )
    {
        if( isOpenBunker )
            OpenBunker( );
        else
            CloseBunker( );
    }

    public void OpenBunker( )
    {
        animator.runtimeAnimatorController = openBunker;
    }
    public void CloseBunker( )
    {
        animator.runtimeAnimatorController = closeBunker;
    }
}
