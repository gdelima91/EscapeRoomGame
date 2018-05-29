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
    public bool oneWay = true;

    [Header("Bunkers")]
    public Bunker[] openBunkers;
    public Bunker[] closeBunkers;

    [Header("Vault Door")]
    public DoorManager doorManager;

    private bool hasActivatedDoorManager = false;
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
                if (!hasActivatedDoorManager && doorManager != null) {
                    hasActivatedDoorManager = true;
                    doorManager.UnLock();
                }
            }
        }
        else
        {
            animator.runtimeAnimatorController = offAnim;

            if (killVolume != null) {
                killVolume.Set_B_Active(false);
            }

            if (electricity.isGettingElectricity) {
                DoorMove();
            }
        }
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

        DoorMove( );
    }

    // Fix Validation
    private void DoorMove( )
    {
        if( isTurnedOn )
        {
            for( int i = 0; i < openBunkers.Length; i++ )
            {
                if( openBunkers[i] != null )
                {
                    OpenBunker( openBunkers[i] );
                }
            }
            for( int i = 0; i < closeBunkers.Length; i++ )
            {
                if( closeBunkers[i] != null )
                {
                    CloseBunker( closeBunkers[i] );
                }
            }
        }
        else if( !oneWay )
        {
            for( int i = 0; i < closeBunkers.Length; i++ )
            {
                if( closeBunkers[i] != null )
                {
                    OpenBunker( closeBunkers[i] );
                }
            }
            for( int i = 0; i < openBunkers.Length; i++ )
            {
                if(openBunkers[i] != null )
                {
                    CloseBunker(openBunkers[i] );
                }
            }
        }

        //if (isTurnedOn)
        //{
        //    if (doorToOpen1 != null && openDoor1Transform != null)
        //        OpenDoor(doorToOpen1, openDoor1Transform);
        //    if (doorToOpen2 != null && openDoor2Transform != null)
        //        OpenDoor(doorToOpen2, openDoor2Transform);
        //    if (doorToClose1 != null && doorCloseTransform1 != null)
        //        CloseDoor(doorToClose1, doorCloseTransform1);
        //    if (doorToClose2 != null && doorCloseTransform2 != null)
        //        CloseDoor(doorToClose2, doorCloseTransform2);
        //}
        //else if (!oneWay) {
        //    if (doorToClose1 != null && openDoor1Transform != null)
        //        OpenDoor(doorToClose1, openDoor1Transform);
        //    if (doorToClose2 != null && openDoor2Transform != null)
        //        OpenDoor(doorToClose2, openDoor2Transform);
        //    if (doorToOpen1 != null && doorCloseTransform1 != null)
        //        CloseDoor(doorToOpen1, doorCloseTransform1);
        //    if (doorToOpen2 != null && doorCloseTransform2 != null)
        //        CloseDoor(doorToOpen2, doorCloseTransform2);
        //}
    }
    private void CheckForPower () {
        if (isTurnedOn && electricity.isGettingElectricity) {
            DoorMove();
        }
    }

    void OpenBunker( Bunker b )
    {
        if( b.GetComponent<AudioSource>() != null )
        {
            b.GetComponent<AudioSource>().Play( );
        }
        b.OpenBunker( );
    }
    void CloseBunker( Bunker b )
    {
        b.CloseBunker( );
    }

    private void OpenDoor (GameObject _doorToOpen, Transform _openDoorTransform) {
        if (_doorToOpen.GetComponent<AudioSource>() != null) {
            _doorToOpen.GetComponent<AudioSource>().Play();
        }
        _doorToOpen.transform.position = _openDoorTransform.position;
        _doorToOpen.transform.rotation = _openDoorTransform.rotation;
    }

    private void CloseDoor (GameObject _doorToClose, Transform _doorCloseTransform) {
        if (_doorCloseTransform != null) {
            _doorToClose.transform.position = _doorCloseTransform.position;
            _doorToClose.transform.rotation = _doorCloseTransform.rotation;
        }
    }

    private void CloseDoor (GameObject _doorToClose) {
        _doorToClose.transform.localPosition = Vector3.zero;
        _doorToClose.transform.localEulerAngles = Vector3.zero;
    }

    public bool GetIsReceivingPower () {
        return isReceivingPower;
    }

    public void SetIsRecevingPower (bool b) {
        isReceivingPower = b;

        CheckForPower();
    }
}
