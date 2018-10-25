using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class comment : MonoBehaviour {


    private float SpawnTime;
    public float TimeInSecondsTillDestroyed;
    public Sprite[] images;
    public GameObject imageObject;
 
	// Use this for initialization
	void Start () {
        SpawnTime = Time.time + TimeInSecondsTillDestroyed;
        imageObject.GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
	}
	
	// Update is called once per frame
	void Update () {
        if (SpawnTime < Time.time)
        {
            Destroy(gameObject);
        }
	}
}
