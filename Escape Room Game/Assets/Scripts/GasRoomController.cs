using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasRoomController : MonoBehaviour {

    public ParticleSystem[] gasParticles;

    private void Start() {
        ChangeParticleStatus(true);
        //StartCoroutine(Wait());
    }

    public void ChangeParticleStatus (bool isOff) {
        foreach (ParticleSystem particle in gasParticles) {
            if (isOff) {
                ParticleSystem.EmissionModule system = particle.emission;
                system.enabled = false;
                particle.transform.GetChild(0).GetComponent<GasDamage>().isActive = false;
            } else if (!isOff) {
                ParticleSystem.EmissionModule system = particle.emission;
                system.enabled = true;
                particle.transform.GetChild(0).GetComponent<GasDamage>().isActive = true;
            }
        }

        print(isOff);
    }

    IEnumerator Wait () {
        yield return new WaitForSeconds(15);
        ChangeParticleStatus(false);
        print("changed");
    }
}
