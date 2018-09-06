using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class Person : MonoBehaviour {

    public bool open;
    public GameManager game;
    public CommunityHealth health;
    public int minHappiness;
    //persons below will most likely have to be individual gameobjects so each 
    //person animation spritesheet is associated with each individual.
    public Sprite[] persons;
    private SpriteRenderer personRenderer;


    // Use this for initialization
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        health = game.GetComponent<CommunityHealth>();
        personRenderer = gameObject.transform.Find("personSprite").GetComponent<SpriteRenderer>();
        RandomPerson();
    }

    void RandomPerson()
    {
        Debug.Log("random person");
        personRenderer.sprite = persons[Random.Range(0, persons.Length)];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
