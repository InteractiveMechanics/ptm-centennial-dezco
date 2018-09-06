using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;

public class SpawnTangibleCollider : MonoBehaviour
{

    public GameObject puckObject;
    public IsoWorld isoWorld;
    public GameObject board;
    private Transform boardHolder;
    private Vector2 mousePosition;
    private GameObject puck;
    private Vector3 puckLocation3D = new Vector3(0f, 0f, 0f);
    private IsoObject iso;
    public int puckIndex;
    private TangibleInfo puckData;

    // Use this for initialization
    void Start()
    {
        isoWorld = FindObjectOfType<IsoWorld>();
        boardHolder = isoWorld.transform;
        puck = Instantiate(puckObject, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        puckData = puck.GetComponent<TangibleInfo>();
        puckData.tileIndex = puckIndex;
        iso = puck.GetComponent<IsoObject>();
        puck.transform.SetParent(boardHolder);
    }

    // Update is called once per frame
    void Update()
    {
        puckLocation3D = isoWorld.ScreenToIso(gameObject.transform.position);
        Debug.Log(puckLocation3D);
        iso.position = puckLocation3D;
        //Debug.Log(puckLocation3D+", "+Input.mousePosition.x+", "+Input.mousePosition.y);

    }
}
