using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable{

    public float throwMultiplier;
    public bool shouldHoldPos = true;
    public bool shouldRotate = true;
    public float inspectDistance = 1;

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
                transform.rotation = Camera.main.transform.rotation;
                rb.MoveRotation(Camera.main.transform.rotation);
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
