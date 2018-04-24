using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour {
    public float distance;
    Vector3 endPos;
    Vector3 startPos;
    public GameObject Piston01;
    public GameObject Piston02;
    public GameObject Piston03;
    public float pistonsN = 0;
    public float lerpTime = 5;
    public float curLerpTime = 0;
    // Use this for initialization
    void Start()
    {
        startPos = Piston01.transform.position;
        endPos = Piston01.transform.position + Vector3.right * distance;

        startPos = Piston02.transform.position;
        endPos = Piston02.transform.position + Vector3.right * distance;

        startPos = Piston03.transform.position;
        endPos = Piston03.transform.position + Vector3.right * distance;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLocalPos();
    }

    void MoveLocalPos()
    {
        if (pistonsN == 1)
        {
            Piston01.name = "Piston01";
            curLerpTime += Time.deltaTime;
            if (curLerpTime >= lerpTime)
            {
                curLerpTime = lerpTime;
            }

            float Perc = curLerpTime / lerpTime;
            Piston01.transform.position = Vector3.Lerp(startPos, endPos, Perc);
        }


        else if (pistonsN == 2)
        {
            Piston02.name = "Piston02";
            curLerpTime += Time.deltaTime;
            if (curLerpTime >= lerpTime)
            {
                curLerpTime = lerpTime;
            }
            float Perc = curLerpTime / lerpTime;
            Piston02.transform.position = Vector3.Lerp(startPos, endPos, Perc);
        }
        else if (pistonsN == 3)
        {
            Piston03.name = "Piston03";
            curLerpTime += Time.deltaTime;
            if (curLerpTime >= lerpTime)
            {
                curLerpTime = lerpTime;
            }
            float Perc = curLerpTime / lerpTime;
            Piston03.transform.position = Vector3.Lerp(startPos, endPos, Perc);
        }

    }
 }
    
/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour {
    public Transform target;
    public float speed;

    public GameObject youWinText;
    public int Piston;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveLocalPos();
	}

    void MoveLocalPos()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

    }
}

     */
