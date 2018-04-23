using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    private FPSRay fpsRay;

	// Use this for initialization
	void Start () {
        fpsRay = GetComponent<FPSRay>();
	}
	
	// Update is called once per frame
	void Update () {
        ThrowController();
	}

    void ThrowController () {
        if (Input.GetButtonDown("Throw")) {
            print("thrown");
            if (fpsRay != null) {
                if (fpsRay.isPickepObj) {
                    fpsRay.isPickepObj = false;

                    

                    if (fpsRay.GetHighlightedGO().GetComponent<PickUp>() != null) {
                        fpsRay.GetHighlightedGO().GetComponent<PickUp>().DropObj();
                    }

                    fpsRay.GetHighlightedGO().GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * fpsRay.GetHighlightedGO().GetComponent<PickUp>().throwMultiplier, ForceMode.VelocityChange);
                }
            }
        }
    }
}
