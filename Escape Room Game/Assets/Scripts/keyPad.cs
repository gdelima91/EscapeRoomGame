using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPad : MonoBehaviour {

    public string curPassword = "1211";// password
    public string input;
    public bool onTrigger;
    public bool openDoor;
    public bool keyPadScreen;
    public Transform doorHinge;
    
    void OnTriggerEnter(Collider other) //trigger box should add in manual key from key's
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)// colison box input
    {
        onTrigger = false;
        keyPadScreen = false;
        input = "";
    }

    void Update() // needs to be modified, for some reason the door don't rotate to doorHinge at -90, if we wan't it to be pull back then we can just the location that it would stop on
    {
        if (!openDoor)
        {
            if (onTrigger)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    keyPadScreen = true;
                    onTrigger = false;
                }
            }

            if (keyPadScreen) // keypad and it's input ! problem with input|| it has to be input = input +"" to remember the previous input, but this has a problem with keypads it makes 4 copies of the input
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    //isDone = true;
                    input = input + "1";
                }
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    input = input + "2";
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    input = input + "3";
                }
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    input = input + "4";
                }
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    input = input + "5";
                }
                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    input = input + "6";
                }
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    input = input + "7";
                }
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    input = input + "8";
                }
                if ( Input.GetKeyDown(KeyCode.Keypad9))
                {
                    input = input + "9";
                }
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    input = input + "0";
                }
            }
        }

        if (input == curPassword)
        {
            openDoor = true;
        }

        if (openDoor)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
        }
    }

    void OnGUI() // using gui as box buttons will add keypad same as one on keyboard
    {
        if (!openDoor)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Press 'F' to open keypad");
            }

            if (keyPadScreen) // keypad and it's input ! problem with input|| it has to be input = input +"" to remember the previous input, but this has a problem with keypads it makes 4 copies of the input
            {

                GUI.Box(new Rect(0, 0, 320, 400), "");
                GUI.Box(new Rect(5, 5, 310, 25), input);
                if (GUI.Button(new Rect(5, 35, 100, 100), "1"))
                {
                    //isDone = true;
                    input = input + "1";
                }
                if (GUI.Button(new Rect(110, 35, 100, 100), "2"))
                {
                    input = input + "2";
                }
                if (GUI.Button(new Rect(215, 35, 100, 100), "3"))
                {
                    input = input + "3";
                }
                if (GUI.Button(new Rect(5, 140, 100, 100), "4"))
                {
                    input = input + "4";
                }
                if (GUI.Button(new Rect(110, 140, 100, 100), "5"))
                {
                    input = input + "5";
                }
                if (GUI.Button(new Rect(215, 140, 100, 100), "6"))
                {
                    input = input + "6";
                }
                if (GUI.Button(new Rect(5, 240, 100, 100), "7"))
                {
                    input = input + "7";
                }
                if (GUI.Button(new Rect(110, 240, 100, 100), "8"))
                {
                    input = input + "8";
                }
                if (GUI.Button(new Rect(215, 240, 100, 100), "9"))
                {
                    input = input + "9";
                }
                if (GUI.Button(new Rect(110, 350, 100, 100), "0"))
                {
                    input = input + "0";
                }
            }
        }
    } 
}
