using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;

public class TouchInput : MonoBehaviour {

    public GameObject puckObject;
    public IsoWorld isoWorld;
    public GameObject board;
    private Transform boardHolder;
    private Vector2 mousePosition;
    private GameObject puck;
    private Vector3 puckLocation3D = new Vector3(0f, 0f, 0f);
    private IsoObject iso;

	// Use this for initialization
	void Start () {
        isoWorld = FindObjectOfType<IsoWorld>();
        boardHolder = board.transform;
        puck = Instantiate(puckObject, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        iso = puck.GetComponent<IsoObject>();
        puck.transform.SetParent(boardHolder);
	}
	
	// Update is called once per frame
	void Update () {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0)){
            puckLocation3D = isoWorld.ScreenToIso(mousePosition, 0f);
        } 
        iso.position = puckLocation3D;
        Debug.Log(puckLocation3D+", "+Input.mousePosition.x+", "+Input.mousePosition.y);
            
	}
}
