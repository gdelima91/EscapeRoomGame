using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLight : MonoBehaviour {

    public float lightIntensity;
    public Material material;

    private new Light light;

    private void Start() {
        light = gameObject.transform.GetChild(0).GetComponent<Light>();
    }

    public void TurnLight (bool turnOn) {
        if (light != null) {
            if (turnOn) {
                GetComponent<Renderer>().material = material;
                StartCoroutine(SetTurnLight());
            } else {
                light.intensity = 0;
            }
        }
    }

    IEnumerator SetTurnLight () {
        while (light.intensity < lightIntensity) {
            light.intensity = Mathf.Lerp(light.intensity, lightIntensity, Time.deltaTime);
            yield return null;
        }
    }
}
