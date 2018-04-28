using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;

public class Inspect : MonoBehaviour {

    public float speed = 1;
    public float inspectDistance;

    private FirstPersonController fpsController;
    private CharacterController characterController;
    private FPSRay fPSRay;
    private crouch _crouch;
    private Throw _throw;
    private Camera mCamera;
    private bool isInspecting;
    private GameObject go;

	// Use this for initialization
	void Start () {
        if (GetComponent<FirstPersonController>() != null)  { fpsController = GetComponent<FirstPersonController>(); }
        if (GetComponent<CharacterController>() != null)    { characterController = GetComponent<CharacterController>(); }
        if (GetComponent<FPSRay>() != null)                 { fPSRay = GetComponent<FPSRay>(); }
        if (GetComponent<crouch>() != null)                 { _crouch = GetComponent<crouch>(); }
        if (GetComponent<Throw>() != null)                  { _throw = GetComponent<Throw>(); }
        mCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        InputCheck();
        RotateInspect();
	}

    void SetInspectMode (bool b) {

        if (fpsController != null) {
            fpsController.enabled = !b;
        }

        if (characterController != null) {
            characterController.enabled = !b;
        }

        if (fPSRay != null) {
            fPSRay.enabled = !b;
        }

        if (_crouch != null) {
            _crouch.enabled = !b;
        }

        if (_throw != null) {
            _throw.enabled = !b;
        }

        isInspecting = b;

        if (isInspecting) {
            go.GetComponent<PickUp>().DeactivateHighlight();
            SetInspectDistance();
        } else {
            go.GetComponent<PickUp>().ActivateHighLight();
        }
    }

    void SetInspectDistance () {
        Ray ray = new Ray(mCamera.transform.position, mCamera.transform.TransformDirection(Vector3.forward));
        Vector3 inspectPosition = ray.origin + (ray.direction.normalized * go.GetComponent<PickUp>().inspectDistance);
        go.transform.position = inspectPosition;
    }

    void RotateInspect () {

        if (Input.GetButton("Click") &&
            isInspecting) {
            //go.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);

            float rotx = (Input.GetAxis("Mouse X") * Time.deltaTime * speed) * Mathf.Deg2Rad;
            float roty = (Input.GetAxis("Mouse Y") * Time.deltaTime * speed) * Mathf.Deg2Rad;

            go.transform.Rotate(Vector3.up, rotx, Space.Self);
            go.transform.Rotate(Vector3.right, roty, Space.Self);
            

            string output = Input.GetAxis("Mouse Y").ToString() + ", " + Input.GetAxis("Mouse X").ToString();
            print(output);
        }
    }

    void InputCheck () {

        if (Input.GetButtonDown("Inspect") && fPSRay.GetHighlightedGO() != null && fPSRay.isPickepObj) {
            if (isInspecting) {
                go.GetComponent<PickUp>().shouldRotate = true;
                go.GetComponent<PickUp>().shouldHoldPos = true;
                SetInspectMode(false);
            } else {
                go = fPSRay.GetHighlightedGO();
                go.GetComponent<PickUp>().shouldRotate = false;
                go.GetComponent<PickUp>().shouldHoldPos = false;
                SetInspectMode(true);
            }
        }
    }
}
