using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;
using IsoTools.Physics;

public class PromptLocation : MonoBehaviour {

    private Color originalAlpha;
    private Color triggerAlpha;
    public GameObject childObject;
    public GameObject tile;

	// Use this for initialization
	void Start () {
        originalAlpha = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        triggerAlpha = originalAlpha;
        triggerAlpha.a = 1f;

	}

    void OnIsoTriggerEnter(IsoCollider other)
    {
        TangibleInfo otherObjectInfo = other.gameObject.GetComponent<TangibleInfo>();
        if (otherObjectInfo){
            tile = otherObjectInfo.prefabTile;
            gameObject.transform.Find("locationObject").gameObject.SetActive(false);
            childObject = Instantiate(tile, gameObject.transform);
            childObject.SetActive(true);
            IsoObject iso = childObject.GetComponent<IsoObject>();
            iso.position = gameObject.GetComponent<IsoObject>().position;
        }


    }

    void OnIsoTriggerExit(IsoCollider other)
    {
        gameObject.transform.Find("locationObject");
        //gameObject.GetComponentInChildren<SpriteRenderer>().color = originalAlpha;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
