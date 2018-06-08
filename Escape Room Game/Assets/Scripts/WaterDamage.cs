using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GasMaskController.instance.DamageGas();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GasMaskController.instance.CanBreath();
        }
    }
}
