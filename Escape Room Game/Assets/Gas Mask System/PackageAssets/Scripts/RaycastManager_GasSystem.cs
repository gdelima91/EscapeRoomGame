using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaycastManager_GasSystem : MonoBehaviour
{
    private GameObject raycasted_obj;

    [Header("Raycast Length/Layer")]
    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image uiCrosshair;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if(hit.collider.CompareTag("GasMask"))
            {
                raycasted_obj = hit.collider.gameObject;
                CrosshairActive();

                if (Input.GetKeyDown("e"))
                {
                    AudioManager.instance.Play("Pickup");
                    GasMaskController.instance.hasGasMask = true;
                    GasMaskController.instance.UpdateMaskUI("MaskWhite");
                    raycasted_obj.SetActive(false);
                }
            }

            else if (hit.collider.CompareTag("GasMaskFilter"))
            {
                raycasted_obj = hit.collider.gameObject;
                CrosshairActive();

                if (Input.GetKeyDown("e"))
                {
                    AudioManager.instance.Play("Pickup");
                    GasMaskController.instance.maskFilters++;
                    GasMaskController.instance.UpdateFilterUI("FilterNumber");
                    raycasted_obj.SetActive(false);
                }
            }
        }

        else
        {
            CrosshairNormal();
        }
    }

    void CrosshairActive()
    {
        uiCrosshair.color = Color.red;
    }

    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }
}
