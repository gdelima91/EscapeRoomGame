using UnityEngine;
using System.Collections;

public class GasDamage : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !GasMaskController.instance.gasMaskOn)
        {
            GasMaskController.instance.DamageGas();
        }

        else if (other.CompareTag("Player") && GasMaskController.instance.gasMaskOn)
        {
            GasMaskController.instance.CanBreath();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !GasMaskController.instance.gasMaskOn)
        {
            GasMaskController.instance.CanBreath();
        }
    }
}
