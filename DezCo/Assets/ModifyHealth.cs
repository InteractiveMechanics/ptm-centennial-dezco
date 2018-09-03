using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyHealth : MonoBehaviour {

    public CommunityHealth healthScript;
    public int happiness;
    public int environment;
    public int budget;



    // Use this for initialization
    void Awake()
    {
        //healthScript = GetComponent<BoardManager>();
        //healthScript.AdjustHealth(happiness, environment, budget);
    }


}
