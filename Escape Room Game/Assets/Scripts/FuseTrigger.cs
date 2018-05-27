using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseTrigger : MonoBehaviour {

    
    public Electricity[] thingsToPower;
    public Lever leverToPower;
    public int fuseCount = 0;

    private FPSRay playerGO;
    private Transform[] fuseSlots;
    

    // Use this for initialization
    void Start() {
        fuseSlots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            fuseSlots[i] = transform.GetChild(i).transform;
            //print(i);
        }
        playerGO = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSRay>();

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {

        // Check if its a fuse item
        if (other.GetComponent<FuseItem>() != null && fuseCount <= fuseSlots.Length) {

            // Check if PickUp script is on it, if it is, then we set isPickedUp to false.
            if (other.GetComponent<PickUp>() != null) {
                other.GetComponent<PickUp>().SetIsPickedUp(false);
                other.GetComponent<PickUp>().DeactivateHighlight();
                playerGO.SetHighlightedGO(null);
                Destroy(other.GetComponent<Interactable>());
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
                for (int i = 0; i < thingsToPower.Length; i++) {
                    thingsToPower[i].isGettingElectricity = true;
                }
            }
        }
    }

}
