using UnityEngine;
using System.Collections;

public class GasDamage : MonoBehaviour
{
    public bool isActive = true;

    void OnTriggerStay(Collider other)
    {
        if (isActive) {
            if (other.CompareTag("Player") && !GasMaskController.instance.gasMaskOn) {
                GasMaskController.instance.DamageGas();
            }

            else if (other.CompareTag("Player") && GasMaskController.instance.gasMaskOn) {
                GasMaskController.instance.CanBreath();
            }
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
