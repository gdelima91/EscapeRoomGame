using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSRay : MonoBehaviour {

    public float maxRayDistance;
    public float minHoldDistance;

    private Camera mCamera;
    private GameObject highlightedGO;
    private GameObject heldGO;
    private GameObject holdPos;
    private float holdPosDistance;
    private float newDistance;
    private RaycastHit hit;
    private Vector3 defaultHoldPos;
    

    //[HideInInspector]
    public bool isPickepObj = false;

	// Use this for initialization
	void Start () {
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
    }

    void ShootRay () {
        
        Debug.DrawRay(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward) * 5, Color.red);

        // Shoot ray
        if (Physics.Raycast(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward), out hit, maxRayDistance)) {

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

                holdPos.transform.position = mCamera.transform.position + (mCamera.transform.forward.normalized * ((Mathf.Clamp(hit.distance - 1.5f, 0, maxRayDistance))));
                //print(Mathf.Clamp(hit.distance, minHoldDistance, maxRayDistance));

            }
            else {
                holdPos.transform.localPosition = new Vector3(0, 0, 1.5f);

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

                        //print("drop");
                    }
                }

            } else {
                if (highlightedGO != null) {
                    if (highlightedGO.GetComponent<PickUp>() != null) {
                        isPickepObj = true;
                        highlightedGO.GetComponent<PickUp>().PickUpObj();
                        highlightedGO.GetComponent<PickUp>().SetHoldPos(holdPos);
                        highlightedGO.transform.parent = null;
                    }
                    else if( highlightedGO.GetComponent<Lever>() != null )
                    {
                        Lever lever = highlightedGO.GetComponent<Lever>();
                        lever.InteractWithLever( );
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
