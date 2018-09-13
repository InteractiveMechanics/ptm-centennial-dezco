using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools.Physics;


//rename to tangibleCollider when possible
public class TangibleCollider : MonoBehaviour {

    //public GameObject prefabTile;
	//public GameObject[] tiles;
	public int tileIndex;
    public bool isCollided;

    void OnEnable()
    {
        isCollided = false;
    }


    void OnIsoTriggerEnter(IsoCollider other){
        if (other.tag == "PromptLocation")
        {
            isCollided = true;
        }
    }




    void OnIsoTriggerExit(IsoCollider other)
    {
        
        if(other.tag == "PromptLocation"){
            isCollided = false;
        }

    }

	
	//Use this for initialization
	void Start () {
        //prefabTile = tiles[tileIndex];
	}

	//void GameObject tileFromIndex(int index){
	//}
		
	
	// Update is called once per frame
	void Update () {
        
	}
}
