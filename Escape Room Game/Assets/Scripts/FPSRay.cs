﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSRay : MonoBehaviour {

    public float maxRayDistance;
    public float minHoldDistance;

    public GameObject testGO;

    [Header("Sound Effects")]
    public AudioClip pickUpAudioClip;
    public AudioClip dropAudioClip;


    private Camera mCamera;
    private GameObject highlightedGO;
    private GameObject heldGO;
    private GameObject holdPos;
    private float holdPosDistance;
    private float newDistance;
    private RaycastHit hit;
    private Vector3 defaultHoldPos;

    private Collider[] colliders;
    private float cameraDistance;
    

    //[HideInInspector]
    public bool isPickepObj = false;

	// Use this for initialization
	void Start () {
        //colliders = new Collider[];
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mCamera = Camera.main;
        holdPos = GameObject.Find("HoldPos");

        if (holdPos != null) {
            holdPosDistance = Vector3.Distance(mCamera.transform.position, holdPos.transform.position);
            defaultHoldPos = holdPos.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        ShootRay();
        
        InteractControls();
        DynamicHoldPos();
        print(hit.collider.bounds.extents.magnitude);
        //if (highlightedGO != null) {
        //    colliders = Physics.OverlapBox(highlightedGO.gameObject.transform.position, highlightedGO.gameObject.GetComponent<Collider>().bounds.extents);
        //    foreach (Collider collider in colliders) {
        //        print(collider.gameObject);
        //    }
        //}
        
    }

    void ShootRay () {
        
        Debug.DrawRay(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward) * 5, Color.red);

        // Shoot ray
        if (Physics.Raycast(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward), out hit, maxRayDistance)) {

            print(hit.collider.gameObject);

            // Check if the object we're hitting is "Interactable"
            if (hit.collider.gameObject.GetComponent<Interactable>() != null) {

                // If this object is not a new object, we highlight
                if (hit.collider.gameObject != highlightedGO && !isPickepObj) {
                    if (highlightedGO != null) {
                        highlightedGO.GetComponent<Interactable>().DeactivateHighlight();
                    }
                    highlightedGO = hit.collider.gameObject;
                    highlightedGO.GetComponent<Interactable>().ActivateHighLight();
                }
            }
            else if (!isPickepObj) {
                if (highlightedGO != null) {
                    highlightedGO.GetComponent<Interactable>().DeactivateHighlight();
                    highlightedGO = null;
                }
            }
        }

        else if (!isPickepObj) {
            if (highlightedGO != null) {

                highlightedGO.GetComponent<Interactable>().DeactivateHighlight();
                highlightedGO = null;
            }
        }
    }

    void DynamicHoldPos () {
        if (isPickepObj) {
            if (hit.distance <= maxRayDistance &&
            hit.collider != null) { 
            //&& Physics.OverlapBox(hit.collider.gameObject.transform.position, hit.collider.gameObject.GetComponent<Collider>().bounds.extents, hit.collider.transform.rotation).Length <= 0) {
                cameraDistance = hit.distance - (hit.collider.bounds.extents.magnitude / 5);

                Vector3 destination = mCamera.transform.position + (mCamera.transform.forward.normalized * ((Mathf.Clamp(hit.distance - 1, 0, cameraDistance))));

                holdPos.transform.position = Vector3.Lerp(holdPos.transform.position, destination, Time.deltaTime * 5);
                //print(Mathf.Clamp(hit.distance, minHoldDistance, maxRayDistance));

            }
            else {
                holdPos.transform.localPosition = new Vector3(0, 0, 1.5f);

            }
        }
    }

    void InteractControls () {

        if (Input.GetButtonDown("Click")) {

            // Drop the held object
            if (isPickepObj) {
                if (highlightedGO != null) {
                    if (highlightedGO.GetComponent<PickUp>() != null && Physics.OverlapBox(highlightedGO.gameObject.transform.position, highlightedGO.gameObject.GetComponent<Collider>().bounds.extents / 2, highlightedGO.transform.rotation).Length <= 1) {// && highlightedGO.GetComponent<Collider>().bounds.Inte) {

                        isPickepObj = false;
                        highlightedGO.GetComponent<PickUp>().DropObj();
                        highlightedGO.GetComponent<PickUp>().SetHoldPos(null);

                        if (dropAudioClip!= null && GetComponent<AudioSource>()) {
                            GetComponent<AudioSource>().PlayOneShot(dropAudioClip);
                        }

                        //print("drop");
                    }
                }
            // Pick up the highlighted object
            } else {
                if (highlightedGO != null) {
                    if (highlightedGO.GetComponent<PickUp>() != null) {
                        isPickepObj = true;
                        highlightedGO.GetComponent<PickUp>().PickUpObj();
                        highlightedGO.GetComponent<PickUp>().SetHoldPos(holdPos);

                        if (pickUpAudioClip != null && GetComponent<AudioSource>() != null) {
                            GetComponent<AudioSource>().PlayOneShot(pickUpAudioClip);
                        }
                        //DontDestroyOnLoad(highlightedGO);
                        //highlightedGO.transform.parent = null;
                    }
                    else if( highlightedGO.GetComponent<Lever>() != null )
                    {
                        Lever lever = highlightedGO.GetComponent<Lever>();
                        lever.InteractWithLever( );
                    }
                    else if (highlightedGO.GetComponent<LockerScript>() != null) {
                        LockerScript lockerScript = highlightedGO.GetComponent<LockerScript>();
                        lockerScript.ChangeState(!lockerScript.currentStatus);
                    } else if (highlightedGO.GetComponent<GasMaskPickUp>() != null) {
                        AudioManager.instance.Play("Pickup");
                        GasMaskController.instance.hasGasMask = true;
                        Destroy(highlightedGO);
                    }
                }
            }
        }
    }

    public GameObject GetHighlightedGO () {
        return highlightedGO;
    }

    public void SetHighlightedGO (GameObject go) {
        highlightedGO = go;
    }
}
