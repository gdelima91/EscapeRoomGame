using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRay : MonoBehaviour {

    public float maxRayDistance;

    private Camera mCamera;
    private GameObject highlightedGO;
    private GameObject holdPos;
    private bool isPickepObj = false;

	// Use this for initialization
	void Start () {
        mCamera = Camera.main;
        holdPos = GameObject.Find("HoldPos");
	}
	
	// Update is called once per frame
	void Update () {
        ShootRay();
        InteractControls();
	}

    void ShootRay () {
        RaycastHit hit;

        if (!isPickepObj) {

            // Shoot ray
            if (Physics.Raycast(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward), out hit, maxRayDistance)) {

                // Check if the object we're hitting is "Interactable"
                if (hit.collider.gameObject.GetComponent<Interactable>() != null) {

                    // Check if this object is a new object 
                    if (hit.collider.gameObject != highlightedGO) {

                        // If we had a previously highlighted object, we disable that first.
                        if (highlightedGO != null) {
                            highlightedGO.GetComponent<Interactable>().DeactivateHighlight();
                        }

                        // We mark the current object as the new highlighted object
                        highlightedGO = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<Interactable>().ActivateHighLight();
                        Debug.DrawRay(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                    }

                    // If this object is not a new object, we highlight
                    else {
                        highlightedGO = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<Interactable>().ActivateHighLight();
                        Debug.DrawRay(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                    }


                }
                else {
                    if (highlightedGO != null) {
                        highlightedGO.GetComponent<Interactable>().DeactivateHighlight();
                    }
                }
            }
            else {
                if (highlightedGO != null) {

                    highlightedGO.GetComponent<Interactable>().DeactivateHighlight();
                    highlightedGO = null;
                }
            }
        }
    }

    void InteractControls () {
        if (Input.GetButtonDown("Click")) {
            
            if (isPickepObj) {
                if (highlightedGO != null) {
                    if (highlightedGO.GetComponent<PickUp>() != null) {
                        isPickepObj = false;
                        highlightedGO.GetComponent<PickUp>().DropObj();
                        highlightedGO.GetComponent<PickUp>().SetHoldPos(null);
                    }
                }

            } else {
                if (highlightedGO != null) {
                    if (highlightedGO.GetComponent<PickUp>() != null) {
                        isPickepObj = true;
                        highlightedGO.GetComponent<PickUp>().PickUpObj();
                        highlightedGO.GetComponent<PickUp>().SetHoldPos(holdPos);
                    }
                }
            }
        }
    }
}
