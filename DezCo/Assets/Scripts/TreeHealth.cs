using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour {

    public bool open;
    public GameManager game;
    public CommunityHealth health;
    public int healthy;
    public int stagnant;
    public int unhealthy;
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
        if (health.CurrentEnvironment < unhealthy)
        {
            renderer.sprite = deadTree;
        }
        else if(health.CurrentEnvironment < stagnant)
        {
            renderer.sprite = dyingTree;
        } else 
        {
            renderer.sprite = healthyTree;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnvironment();

    }
}
