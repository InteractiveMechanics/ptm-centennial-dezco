using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsoTools;
using IsoTools.Physics;

public class SpawnTangibleCollider : MonoBehaviour
{
    
    public GameObject puckObject;
    private IsoWorld isoWorld;
    private GameObject board;
    private Transform boardHolder;
    private Vector2 mousePosition;
    public GameObject puck;
    private Vector3 puckLocation3D = new Vector3(0f, 0f, 0f);
    private IsoObject iso;
    public int puckIndex;
    private TangibleCollider puckData;
    public GameObject incorrectPanel;

    // Use this for initialization
    void Start()
    {
        isoWorld = FindObjectOfType<IsoWorld>();
        boardHolder = isoWorld.transform;
        puck = Instantiate(puckObject, new Vector3(0f, 0f, 0f), Quaternion.identity, boardHolder) as GameObject;
        puckData = puck.GetComponent<TangibleCollider>();
        puckData.tileIndex = puckIndex;
        iso = puck.GetComponent<IsoObject>();

    }

    void CheckPuckCollision(){
        if (puck.gameObject.activeSelf){
            if(!puckData.isCollided){
                incorrectPanel.SetActive(true);
                //puckData.showIncorrect();
            }else if(puckData.isCollided){
                incorrectPanel.SetActive(false);
            } else {
                
            }
        }
    }

    // Update is called once per frame
    public void Update()
    {
        CheckPuckCollision();

            //iso.position = new Vector3(-1f, -1f, 0f);
            puckData.tileIndex = puckIndex;
            puckLocation3D = isoWorld.ScreenToIso(gameObject.transform.position);
            //adjust to offset value
            puckLocation3D = new Vector3(puckLocation3D.x - (iso.sizeX / 2), puckLocation3D.y - (iso.sizeY / 2), puckLocation3D.z);
            //Debug.Log(puckLocation3D);
            iso.position = puckLocation3D;
            //Debug.Log(puckLocation3D+", "+Input.mousePosition.x+", "+Input.mousePosition.y);

            puck.gameObject.SetActive(true);
  

    }

    public void OnDestroy()
    {
        Debug.Log("OnDestroy");
        Destroy(puck);
    }
}
