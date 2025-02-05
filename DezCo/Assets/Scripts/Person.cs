﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class Person : MonoBehaviour {

    public bool open;
    public GameManager game;
    public CommunityHealth health;
    //persons below will most likely have to be individual gameobjects so each 
    //person animation spritesheet is associated with each individual.
    public Sprite[] persons;
    public SpriteRenderer personRenderer;
    public GameObject reaction;
    Animator anim;
    public float maxUpdateInterval;
    public float minUpdateInterval;
    public int happinessThreshold;


    // Use this for initialization
    void Start()
    {
        anim = reaction.gameObject.GetComponent<Animator>();
        RandomPerson();
        //health = game.GetComponent<CommunityHealth>();
        InvokeRepeating("CheckHappiness", Random.Range(minUpdateInterval, maxUpdateInterval), 10);

    }

    void CheckHappiness(){
        Debug.Log(health);
        if (health.CurrentHappiness < happinessThreshold)
        {
            
                Sad();

            
        }
        else if (health.CurrentHappiness > happinessThreshold)
        {
            
                Happy();

        }
    }


    public void Happy(){
        Debug.Log("Happy!");
        //if (stateInfo.fullPathHash == idleHash)
        //{
            
            //anim.SetTrigger(happyHash);
        anim.Play("reaction-happy");
        //}

    }

    public void Sad(){
        Debug.Log("Sad!");
        //if (stateInfo.fullPathHash == idleHash)
        //{
            anim.Play("reaction-unhappy");
        //}

    }



    void RandomPerson()
    {
        

        personRenderer.sprite = persons[Random.Range(0, persons.Length)];
    }



    // Update is called once per frame
    void Update()
    {
        

    }

    void OnDestroy()
    {
        CancelInvoke();
    }


}
