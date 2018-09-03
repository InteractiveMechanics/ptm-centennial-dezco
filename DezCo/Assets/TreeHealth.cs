using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour {

    public bool open;
    public GameManager game;
    public CommunityHealth health;
    public int minEnvironment;
    public Sprite healthyTree;
    public Sprite dyingTree;
    public Sprite deadTree;
    private SpriteRenderer renderer;



    // Use this for initialization
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        health = game.GetComponent<CommunityHealth>();
        renderer = transform.Find("sprite").GetComponent<SpriteRenderer>();

    }

    void CheckEnvironment()
    {
        if (health.CurrentEnvironment < minEnvironment)
        {
            renderer.sprite = dyingTree; //set tree to unhealthy
        }
        else
        {
            renderer.sprite = healthyTree; //set tree to healthy
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnvironment();

    }
}
