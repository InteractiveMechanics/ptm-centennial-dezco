﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;
using IsoTools.Physics;

public class PromptLocation : MonoBehaviour {

    public GameObject locationObject;
    public GameObject childObject;
    public GameObject tile;
    public Prompt prompt;
    public GameObject prefabTile;
    public GameObject[] tiles;
    private Sprite locationSprite;
    public Sprite[] promptIndicators;
    public GameObject constructionObject;
    public float targetTime = 5.0f;
    private bool timerEnd;
    private bool tangibleDown;
    public bool locationOpen = true;
    private GameObject tempColliderObject;
    private IsoBoxCollider iso;
    public int otherIndex;

	void Start () {
        iso = gameObject.GetComponent<IsoBoxCollider>();
	}

    void OnIsoTriggerEnter(IsoCollider other)
    {
        if (other.tag == "TangibleCollider" && locationOpen) {
            
            StartConstruction(other);

        } else if(other.tag == "TangibleCollider" ) {
            //need to check if puck and tile are of same type before startconstruction
        }else {
            Debug.Log(other);
        }

    }

    void StartConstruction(IsoCollider other)
    {
        //get index of puck
        otherIndex = other.GetComponent<TangibleInfo>().tileIndex;

        if (otherIndex < tiles.Length)
        {
            //check to see if tile and prompt category match
            string tileCategory = tiles[otherIndex].GetComponent<ModifyHealth>().type;
            if (tileCategory == prompt.type)
            {
                tempColliderObject = other.gameObject;
                Debug.Log(other.gameObject);
                Debug.Log("otherinfo added" + otherIndex);


                tangibleDown = true;

                //turn on construction animation
                constructionObject.SetActive(true);
                //hide mainprompt
                prompt.hideAll();
                tile = tiles[otherIndex];
                Debug.Log(otherIndex);
                locationObject.SetActive(false);
            }
            else
            {
                //prompt shows incorrect puck panel
                prompt.showIncorrect();
            }
        }
    }

    void timerEnded(){
        timerEnd = true;
        //display return prompt
        prompt.showComplete();
    }


    void OnIsoTriggerExit(IsoCollider other)
    {

        //functionality for initial puck placement
        if (other.tag == "TangibleCollider" && locationOpen){
            int index = other.GetComponent<TangibleInfo>().tileIndex;
            if (index < tiles.Length)
            {
                OnPuckExit(tiles[other.GetComponent<TangibleInfo>().tileIndex]);
            }
            else
            {
                Debug.Log(other);
            }

        }
            
    }


    //separate function from ontriggerexit because collider destroy does not fire exit
    void OnPuckExit(GameObject newTile){
        
        Debug.Log("puckExit!");
            tangibleDown = false;
            //tempCollider = null;
            if (timerEnd)
            {
                    childObject = Instantiate(newTile, gameObject.transform);
                    constructionObject.SetActive(false);
                    //Debug.Log(otherInfo.tileIndex);
                    locationObject.SetActive(false);
                    childObject.SetActive(true);
                    IsoObject childIso = childObject.GetComponent<IsoObject>();
                    childIso.position = gameObject.GetComponent<IsoObject>().position;


                    ModifyHealth health = childObject.GetComponent<ModifyHealth>();
                    //display answer prompt
                    prompt.showResults(health);

                
            }
            else
            {
                prompt.showMain();
                constructionObject.SetActive(false);
                locationObject.SetActive(true);
            }



    }
	
	// Update is called once per frame
	void Update () {
        
        //Debug.Log("otherinfo " + otherObjectInfo+", "+"tempCollider " + otherObjectInfo);

        //if we have info from collision enter but the object no longer exists
        if(tempColliderObject){
            Debug.Log("tempCollider exists!");
            if (!tempColliderObject.activeSelf && tile)
            {
                OnPuckExit(tile);
                tile = null;
            }
        }


        //if childObject is active and prompt is present
        if(prompt && childObject) {
            locationOpen = false;
        }

        if(tangibleDown && !timerEnd){
            //decrease timer when tangible is in place
            targetTime -= Time.deltaTime;
        } else if (!tangibleDown){
            //reset if tangible isn't down
            targetTime = 5.0f;
        }
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        if (!prompt){
            iso.enabled = false;
        } else {
            iso.enabled = true;
        }

        if (prompt){
            
            if (prompt.type == "recreation"){
                locationSprite = promptIndicators[1];
            } else if (prompt.type == "infrastructure"){
                locationSprite = promptIndicators[0];
            } else if (prompt.type == "transportation")
            {
                locationSprite = promptIndicators[2];
            } else {
                locationSprite = null;
            }

        }
        locationObject.GetComponent<SpriteRenderer>().sprite = locationSprite;
	}
}
