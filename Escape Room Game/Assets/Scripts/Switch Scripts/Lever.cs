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
    public Transform openDoorTransform;
    public GameObject doorToClose;
    public Transform doorCloseTransform;
    public bool oneWay = true;

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

        DoorMove();
    }

    private void DoorMove () {
        if (isTurnedOn) {
            if (doorToOpen != null)
                OpenDoor(doorToOpen);
            if (doorToClose != null)
                CloseDoor(doorToClose);
        }
        else if (!oneWay) {
            if (doorToClose != null)
                OpenDoor(doorToClose);
            if (doorToOpen != null)
                CloseDoor(doorToOpen);
        }
    }
    private void CheckForPower () {
        if (isTurnedOn && electricity.isGettingElectricity) {
            OpenDoor(doorToOpen);
        }
    }

    private void OpenDoor (GameObject _doorToOpen) {

            if (_doorToOpen.GetComponent<AudioSource>() != null) {
                _doorToOpen.GetComponent<AudioSource>().Play();
            }
            _doorToOpen.transform.position = openDoorTransform.position;
            _doorToOpen.transform.rotation = openDoorTransform.rotation;

    }

    private void CloseDoor (GameObject _doorToClose) {
        if (doorCloseTransform != null) {
            _doorToClose.transform.position = doorCloseTransform.position;
            _doorToClose.transform.rotation = doorCloseTransform.rotation;
        } else {
            _doorToClose.transform.localPosition = Vector3.zero;
            _doorToClose.transform.localEulerAngles = Vector3.zero;
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
