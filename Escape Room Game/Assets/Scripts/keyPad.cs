using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPad : MonoBehaviour {

    public string curPassword = "1211";// password
    public int codeSize = 0;
    public const int maxCodeSize = 4;
    public string input;
    public bool onTrigger;
    public bool openDoor;
    public bool keyPadScreen;
    public Transform doorHinge;

    private Color guiNumberColor;

   // private Electricity electricity;

    private void Start() {
        //    electricity = GetComponent<Electricity>();
        guiNumberColor = new Color(128, 128, 128);
    }

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
        /* if (codeSize <= maxCodeSize)
         {
             if (Input.GetKeyDown("0"))
             {
                 input += "0";
                 ++codeSize;
             }
             if (Input.GetKeyDown("1"))
             {
                 input += "0";
                 ++codeSize;
             }
         }*/

        if (!openDoor)
        {
            if (onTrigger) //&& electricity.isGettingElectricity)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    keyPadScreen = true;
                    onTrigger = false;
                }
            }

            if (keyPadScreen) // keypad and it's input ! problem with input|| it has to be input = input +"" to remember the previous input, but this has a problem with keypads it makes 4 copies of the input
            {
                if (codeSize < maxCodeSize)
                {
                    if (Input.GetKeyDown(KeyCode.Keypad7))
                    {
                        //isDone = true;
                        input = input + "7";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad8))
                    {
                        input = input + "8";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad9))
                    {
                        input = input + "9";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad4))
                    {
                        input = input + "4";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad5))
                    {
                        input = input + "5";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad6))
                    {
                        input = input + "6";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad1))
                    {
                        input = input + "1";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad2))
                    {
                        input = input + "2";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad3))
                    {
                        input = input + "3";
                        ++codeSize;
                    }
                    if (Input.GetKeyDown(KeyCode.Keypad0))
                    {
                        input = input + "0";
                        ++codeSize;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    if (codeSize > 0)
                    {
                        input = input.Substring(0, codeSize - 1);
                        --codeSize;
                        guiNumberColor = new Color(128, 128, 128);
                    }
                }
                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    if (input != curPassword)
                    {
                        guiNumberColor = new Color(255, 0, 0);
                    }
                    else
                    {
                        openDoor = true;
                    }
                }
            }
        }

        //door hinge rotation
        if (openDoor)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 250);
        }
    }

    void OnGUI() // using gui as box buttons will add keypad same as one on keyboard
    {
        if (!openDoor)
        {
            if (onTrigger)// && electricity.isGettingElectricity)
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Press 'F' to open keypad");
            }

            if (keyPadScreen) // keypad and it's input ! problem with input|| it has to be input = input +"" to remember the previous input, but this has a problem with keypads it makes 4 copies of the input
            {
                GUI.color = new Color(128, 128, 128);
                GUI.Box(new Rect(0, 0, 320, 400), "");

                GUI.color = guiNumberColor;
                GUI.Box(new Rect(5, 5, 310, 25), input);

                GUI.color = new Color(128, 128, 128);

                if (GUI.Button(new Rect(5, 35, 100, 100), "7"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "7";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(110, 35, 100, 100), "8"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "8";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(215, 35, 100, 100), "9"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "9";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(5, 140, 100, 100), "4"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "4";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(110, 140, 100, 100), "5"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "5";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(215, 140, 100, 100), "6"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "6";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(5, 240, 100, 100), "1"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "1";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(110, 240, 100, 100), "2"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "2";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(215, 240, 100, 100), "3"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "3";
                        ++codeSize;
                    }
                }
                if (GUI.Button(new Rect(110, 350, 100, 100), "0"))
                {
                    if (codeSize < maxCodeSize)
                    {
                        input = input + "0";
                        ++codeSize;
                    }
                }

                if (GUI.Button(new Rect(215, 350, 100, 100), "Enter"))
                {
                    if (input != curPassword)
                    {
                        guiNumberColor = new Color(255, 0, 0);
                    }
                    else
                    {
                        openDoor = true;
                    }
                }

                if (GUI.Button(new Rect(5, 350, 100, 100), "BackSpace"))
                {
                    if (codeSize > 0)
                    {
                        input = input.Substring(0, codeSize - 1);
                        --codeSize;
                        guiNumberColor = new Color(128, 128, 128);

                    }
                }

                
            }
        }
    } 
}
