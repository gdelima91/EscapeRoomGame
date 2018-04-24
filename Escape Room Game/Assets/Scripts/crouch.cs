using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouch : MonoBehaviour {
    

    public CharacterController characterController;

    private void Start()
    {
        (characterController) = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.height = 1.0f;
        }
        else
        {
            characterController.height = 2.0f;
        }
    }
}
