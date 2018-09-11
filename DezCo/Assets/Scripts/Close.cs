using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour {



    public bool open;
    public GameManager game;
    public CommunityHealth health;
    public int minBudget;
    private GameObject sign;

    
	// Use this for initialization
	void Start () {
        game = FindObjectOfType<GameManager>();
        sign = GameObject.Find("ClosedSign");
        health = game.GetComponent<CommunityHealth>();
	}

    void CheckBudget()
    {
        if (health.CurrentBudget < minBudget)
        {
            sign.SetActive(true); //set closed sign visible
        }
        else {
            sign.SetActive(false); //hide closed sign
        }
    }
	
	// Update is called once per frame
	void Update () {
        CheckBudget();

	}
}
