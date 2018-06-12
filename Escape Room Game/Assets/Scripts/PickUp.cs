using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PickUp : Interactable{

    public float throwMultiplier;
    public bool shouldHoldPos = true;
    public bool shouldRotate = true;
    public float inspectDistance = 1;
    public bool canBeInspected = false;
    public string inspectBlurb;

    private bool isPickedUp;
    private Rigidbody rb;
    private GameObject holdPos;
    private Collider _collider;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPickedUp) {
            SetPickedUpTransform();
        }
    }

    private void FixedUpdate() {
        
    }

    public void PickUpObj () {
        isPickedUp = true;

        if (rb != null) {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            //rb.isKinematic = true;
        }

        Physics.IgnoreCollision(GameObject.Find("FPSController").GetComponent<CharacterController>().GetComponent<Collider>(), _collider, true);

        if (_collider != null) {
            _collider.isTrigger = true;
        }

        gameObject.layer = 2;
    }

    public void DropObj () {
        isPickedUp = false;

        if (rb != null) {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            //rb.isKinematic = false;
        }

        Physics.IgnoreCollision(GameObject.Find("FPSController").GetComponent<CharacterController>().GetComponent<Collider>(), _collider, false);

        if (_collider != null) {
            _collider.isTrigger = false;
        }

        gameObject.layer = 0;
    }

    void SetPickedUpTransform () {
        if (holdPos != null) {
            if (shouldHoldPos) {
                transform.position = holdPos.transform.position;
                rb.MovePosition(holdPos.transform.position);
            }
            
            if (shouldRotate) {
                Vector3 destination = Camera.main.transform.eulerAngles;
                Vector3 currentRot = transform.rotation.eulerAngles;
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, destination, Time.deltaTime);
                //transform.ro

                transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, Time.deltaTime * 5);

                //transform.eulerAngles = Camera.main.transform.rotation.eulerAngles;

                //rb.MoveRotation(Camera.main.transform.rotation);
            }
        }
    }

    public bool GetIsPickedUp () {
        return isPickedUp;
    }

    public void SetIsPickedUp (bool _isPickedUp) {
        isPickedUp = _isPickedUp;
    }

    public void SetHoldPos (GameObject go) {
        holdPos = go;
    }
}
