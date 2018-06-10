using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header( "Door" )]
    public Animator doorAnimator;
    public RuntimeAnimatorController openDoorController;
    public RuntimeAnimatorController closeDoorController;

    [Header( "Locks" )]
    public Animator[] animators;
    public RuntimeAnimatorController openController;
    public RuntimeAnimatorController closeController;

    [Header("Light Meshes")]
    public GameObject[] lightMeshes;
    public Material openMaterial;
    public Material closedMaterial;

    [Header("Lights")]
    public GameObject[] lights;

    [Header("Light Colors")]
    public Color openColor;
    public Color closedColor;

    int counter = 0;
    public bool isDoorOpen = false;

    void Start( )
    {
        doorAnimator.runtimeAnimatorController = closeDoorController;
        for( int i = 0; i < animators.Length; i++ )
        {
            animators[i].runtimeAnimatorController = closeController;
        }
        counter = 0;
    }

    public void UnLock( )
    {
        if( !isDoorOpen )
        {
            animators[counter].runtimeAnimatorController = openController;

            counter++;

            isDoorOpen = counter < animators.Length ? false : true;
        }
        if( isDoorOpen )
        {
            doorAnimator.runtimeAnimatorController = openDoorController;
        }
        if (lights[counter - 1] != null) {
            lights[counter - 1].GetComponent<Light>().color = openColor;
        }
        if (lightMeshes[counter - 1] != null) {
            lightMeshes[counter - 1].GetComponent<MeshRenderer>().material = openMaterial;
        }
    }
}