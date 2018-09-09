using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IsoTools;

public class Prompt : MonoBehaviour {
    
    //public string text;
    public GameObject detailsObject;
    private string detailsText;
    public IsoWorld isoWorld;
    public PromptLocation prompt;


	// Use this for initialization
	void Start () {
        //GameObject.GetComponent<AudioSource>().Play();
        //detailsObject = GameObject.Find("MainPanel/PromptInfo/PromptDetails").gameObject;
        //Debug.Log(detailsText);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 location3d = prompt.GetComponent<IsoObject>().position;
        Vector2 promptPosition = isoWorld.IsoToScreen(location3d);
        gameObject.GetComponent<RectTransform>().localPosition = new Vector2 (promptPosition.x- 1920f, promptPosition.y-1080f);
        detailsText = (gameObject.transform.position.x + ", " + gameObject.transform.position.y);
        detailsObject.GetComponent<Text>().text = detailsText;


        //Debug.Log(detailsObject.GetComponent<Text>().text);
	}
}
