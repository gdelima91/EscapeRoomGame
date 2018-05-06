using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    public bool isTurnedOn = false;
    public Animator animator;
    public RuntimeAnimatorController offAnim; // up to down anim
    public RuntimeAnimatorController onAnim; // down to up anim
    public GameObject sparks;
    public AudioClip sparksSFX;
    public Transform[] sparksTransform;
    public KillVolume killVolume;
    public GameObject doorToOpen;
    public Transform doorRotation;

    private AudioSource audioSource;
    private bool isReceivingPower = false;
    private Electricity electricity;

    void Start( )
    {
        audioSource = GetComponent<AudioSource>();
        electricity = GetComponent<Electricity>();
        //isTurnedOn = animator.runtimeAnimatorController == offAnim ? false : true;
        if( animator.runtimeAnimatorController == offAnim )
        {
            isTurnedOn = false;
        }
        else
        {
            isTurnedOn = true;
        }
    }

    public void InteractWithLever( )
    {
        isTurnedOn = !isTurnedOn;
        //animator.runtimeAnimatorController = isTurnedOn == true ? onAnim : offAnim;
        if( isTurnedOn )
        {
            animator.runtimeAnimatorController = onAnim;

            if (electricity.isGettingElectricity) {
                StartCoroutine(PlaySparks());
            }
        }
        else
        {
            animator.runtimeAnimatorController = offAnim;

            if (killVolume != null) {
                killVolume.Set_B_Active(false);
            }
        }
        Debug.Log( "Touched lever" );

        
    }

    IEnumerator PlaySparks () {
        yield return new WaitForSeconds(0.75f);
        if (sparks != null && sparksTransform.Length > 0) {
            for (int i = 0; i < sparksTransform.Length; i++) {
                Instantiate(sparks, sparksTransform[i].transform.position, sparksTransform[i].transform.rotation);
            }
        }

        if (sparksSFX != null) {
            audioSource.PlayOneShot(sparksSFX);
        }

        if (killVolume != null) {
            killVolume.Set_B_Active(true);
        }

        OpenDoor();
    }

    private void CheckForPower () {
        if (isTurnedOn && electricity.isGettingElectricity) {
            OpenDoor();
        }
    }

    private void OpenDoor () {
        if(doorToOpen != null) {
            doorToOpen.transform.position = doorRotation.position;
            doorToOpen.transform.rotation = doorRotation.rotation;
        }
    }

    public bool GetIsReceivingPower () {
        return isReceivingPower;
    }

    public void SetIsRecevingPower (bool b) {
        isReceivingPower = b;

        CheckForPower();
    }
}
