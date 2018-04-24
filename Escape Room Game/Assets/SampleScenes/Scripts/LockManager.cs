using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour {

    public int addOne = 1;
    public GameObject Piston;
    public float pistonsN = 0;
   // public Puzzle addScore;



    // Use this for initialization
    void Start ()
    {
     //   addScore = GetComponent<Puzzle>();

    }

	// Update is called once per frame
	void Update ()
    {
        if(pistonsN >=0)
        {
            Done();
        }
	
	}
    void Done()
    {
    //    pistonsN.score += 1;
    }
}
/*    
public static LockManager instance = null;
public bool lockOpen;
public bool doorOpen;
public bool onTrigger;
public Transform doorHinge;
public Transform doorPiston;
public GameObject youWinText;

 */