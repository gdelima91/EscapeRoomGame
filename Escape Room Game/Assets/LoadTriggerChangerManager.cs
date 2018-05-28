using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTriggerChangerManager : MonoBehaviour {

    [Header("Initialization")]
    public GameObject[] initialActiveLoadTriggers;
    public GameObject[] initialDeactiveLoadTriggers;

    // Use this for initialization
    void Start () {
		if (initialActiveLoadTriggers.Length > 0) {
            foreach (GameObject go in initialActiveLoadTriggers) {
                go.SetActive(true);
            }
        }

        if (initialDeactiveLoadTriggers.Length > 0) {
            foreach (GameObject go in initialDeactiveLoadTriggers) {
                go.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
