using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnOffScreen : MonoBehaviour {

    //void OnBecameInvisible()
    //{
        
    //    Destroy(gameObject.transform.parent.gameObject);
    //}

    Renderer m_Renderer;

	// Use this for initialization
	void Start () {
        m_Renderer = GetComponent<Renderer>();
	}


	
	// Update is called once per frame
	void Update () {
        if (!m_Renderer.isVisible){
            Debug.Log("outside screen");
            Destroy(gameObject.transform.parent.gameObject);
        }
	}
}
