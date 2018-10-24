using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;
using IsoTools.Physics;

public class PromptLocation : MonoBehaviour
{

    public GameObject locationObject;
    public GameObject childObject;
    public GameObject tile;
    public Prompt prompt;
    public GameObject prefabTile;
    public GameObject[] tiles;
    private Sprite locationSprite;
    public Sprite[] promptIndicators;
    public GameObject constructionObject;
    public float constructionTime;
    private float targetTime;
    private bool timerEnd;
    private bool tangibleDown;
    public bool locationOpen = true;
    private IsoCollider tempColliderObject;
    private IsoBoxCollider iso;
    public int otherIndex;

    void Start()
    {
        iso = gameObject.GetComponent<IsoBoxCollider>();
    }

    void OnIsoTriggerEnter(IsoCollider other)
    {
        
        if (other.tag == "TangibleCollider" )
        {
            tangibleDown = true;
            tempColliderObject = other;
            if (CheckPuckIndex(other) && locationOpen)
            {
                StartConstruction(other);
            }
            else if (CheckPuckIndex(other))
            {
                //Destroy(childObject);
                StartConstruction(other);
            }
            else
            {
                //prompt.showIncorrect();
                StartConstruction(other);
            }
        }
        else
        {
            Debug.Log(other);
        }

    }


    void OnIsoTriggerExit(IsoCollider other)
    {


        //functionality for initial puck placement
        if (other.tag == "TangibleCollider")
        {
            tangibleDown = false;
            if (CheckPuckIndex(other) && timerEnd)
            {
                //build new tile
                OnPuckExit(tiles[other.GetComponent<TangibleCollider>().tileIndex]);
            } else if (!CheckPuckIndex(other) && childObject && timerEnd) //delete if timer is out at puck is delete puck
            {
                ResetPromptLocation();
            }else if (!childObject)
            {
                ResetPromptLocation();
            }  else {
                prompt.hideAll();
                constructionObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log(other);
        }

    }

    //separate function from ontriggerexit because collider destroy does not fire exit
    void OnPuckExit(GameObject newTile)
    {

        Debug.Log("puckExit!");
        Destroy(childObject);
        childObject = Instantiate(newTile, gameObject.transform);
        constructionObject.SetActive(false);
        locationObject.SetActive(false);
        childObject.SetActive(true);
        IsoObject childIso = childObject.GetComponent<IsoObject>();
        childIso.position = gameObject.GetComponent<IsoObject>().position;
        ModifyHealth health = childObject.GetComponent<ModifyHealth>();
        //display nothing after
        prompt.hideAll();
        tempColliderObject = null;

    }

    bool CheckPuckIndex(IsoCollider other)
    {
        bool notDelete;

        int index = other.GetComponent<TangibleCollider>().tileIndex;

        if (index < tiles.Length)
        {
            notDelete = true;
        } else {
            //if out of range of tiles array the puck must be 'delete'
            notDelete = false;
        }


        return notDelete;
    }

    void StartConstruction(IsoCollider other)
    {
        
        Debug.Log(other.gameObject);
        Debug.Log("otherinfo added" + otherIndex);
        //turn on construction animation
        constructionObject.SetActive(true);
        //hide mainprompt
        prompt.hideAll();
        tile = tiles[otherIndex];
        Debug.Log(otherIndex);
        locationObject.SetActive(false);

    }

    void timerEnded()
    {
        timerEnd = true;
        //display return prompt
        prompt.showComplete();
    }


    void ResetPromptLocation()
    {
        Debug.Log("reset prompt");
        Destroy(childObject);
        prompt.hideAll();
        constructionObject.SetActive(false);
        locationObject.SetActive(true);
    }

    void ResetTimer(){
        targetTime = constructionTime;
        timerEnd = false;
    }





    // Update is called once per frame
    void Update()
    {

        //Debug.Log("otherinfo " + otherObjectInfo+", "+"tempCollider " + otherObjectInfo);

        //if we have info from collision enter but the object is set to inactive
        if(tempColliderObject){
            //Debug.Log("tempCollider exists!");

            if (!tempColliderObject.gameObject.activeSelf)
            {
                Debug.Log("puck lifted");
                tangibleDown = false;

                if (CheckPuckIndex(tempColliderObject) && timerEnd){
                    OnPuckExit(tiles[tempColliderObject.GetComponent<TangibleCollider>().tileIndex]);
                    //OnPuckExit(tile);
                    tempColliderObject = null;
                }
                else if (!CheckPuckIndex(tempColliderObject) && childObject && timerEnd) //delete if timer is out at puck is delete puck
                {
                    ResetPromptLocation();
                }
                else if (!childObject)
                {
                    ResetPromptLocation();

                } else {
                    prompt.hideAll();
                    constructionObject.SetActive(false);
                } 
            } 
        }




        //if (prompt && childObject) //if childObject is active and prompt is present set location open to false
        //{
        //    locationOpen = false;
        //}

        if (tangibleDown && !timerEnd && CheckPuckIndex(tempColliderObject)) //if tangible is placed but timer has not ended
        {
            //decrease timer when tangible is in place
            targetTime -= Time.deltaTime;
        } 
        else if (!tangibleDown) //if tangible is not placed
        {
            ResetTimer();
        }

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

        //disabled for free play

        //if (!prompt)
        //{
        //    iso.enabled = false;
        //}
        //else
        //{
        //    iso.enabled = true;
        //}

        iso.enabled = true;


    }
}
