using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;

public class Wander : MonoBehaviour {

    public float duration;    //the max time of a walking session (set to ten)
    float elapsedTime = 0f; //time since started walk
    float wait = 0f; //wait this much time
    float waitTime = 0f; //waited this much time

    float randomX;  //randomly go this X direction
    float randomY;  //randomly go this Z direction
    IsoObject iso;

    bool move = true; //start moving

    void Start()
    {
        randomX = Random.Range(-3, 3);
        randomY = Random.Range(-3, 3);
        iso = gameObject.GetComponent<IsoObject>();
        Debug.Log(iso.positionX);
    }

    void Update()
    {



        //Debug.Log (elapsedTime);

        if (elapsedTime < duration && move)
        {
            //if its moving and didn't move too much

            iso.position = new Vector3(randomX+iso.positionX, randomY+iso.positionY, 0f) * Time.deltaTime;
            elapsedTime += Time.deltaTime;

        }
        else
        {
            //do not move and start waiting for random time
            move = false;
            wait = Random.Range(5f, 10f);
            waitTime = 0f;
        }

        if (waitTime < wait && !move)
        {
            //you are waiting
            waitTime += Time.deltaTime;


        }
        else if (!move)
        {
            move = true;
            elapsedTime = 0f;
            randomX = Random.Range(-4f, 4f);
            randomY = Random.Range(-4f, 4f);
        }

    }
}
