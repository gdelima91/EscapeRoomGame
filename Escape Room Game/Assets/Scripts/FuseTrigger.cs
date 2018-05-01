using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseTrigger : MonoBehaviour {

    public FPSRay playerGO;
    public Lever leverToPower;

    private Transform[] fuseSlots;
    private int fuseCount = 0;

    // Use this for initialization
    void Start() {
        fuseSlots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            fuseSlots[i] = transform.GetChild(i).transform;
            //print(i);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        print("trigger");

        // Check if its a fuse item
        if (other.GetComponent<FuseItem>() != null) {

            // Check if PickUp script is on it, if it is, then we set isPickedUp to false.
            if (other.GetComponent<PickUp>() != null) {
                other.GetComponent<PickUp>().SetIsPickedUp(false);
            }

            if (other.GetComponent<Rigidbody>() != null) {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
            }
            other.transform.position = fuseSlots[other.GetComponent<FuseItem>().fuseSlot].position;
            other.transform.rotation = fuseSlots[other.GetComponent<FuseItem>().fuseSlot].rotation;
            if (playerGO != null) {
                playerGO.isPickepObj = false;
            }

            fuseCount++;
            if (fuseCount > 3) {
                if (leverToPower != null) {
                    leverToPower.SetIsReceivingPower(true);
                }
            }
        }
    }

}
