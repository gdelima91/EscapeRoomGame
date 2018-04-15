using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public Material highLightMat;

    private Material oldMat;

	// Use this for initialization
	void Awake () {
        oldMat = gameObject.GetComponent<Renderer>().material;
    }

    public void ActivateHighLight () {
        gameObject.GetComponent<MeshRenderer>().material = highLightMat;
    }

    public void DeactivateHighlight() {
        gameObject.GetComponent<MeshRenderer>().material = oldMat;
    }
}
