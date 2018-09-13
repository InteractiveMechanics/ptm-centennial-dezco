using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour {

    public bool open;
    public GameManager game;
    public CommunityHealth health;
    public int stagnant;
    public int unhealthy;
    public Sprite healthyTree;
    public Sprite dyingTree;
    public Sprite deadTree;
    private SpriteRenderer treeRenderer;



    // Use this for initialization
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        health = game.GetComponent<CommunityHealth>();
        treeRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();

    }

    void CheckEnvironment()
    {
        if (health.CurrentEnvironment < unhealthy)
        {
            treeRenderer.sprite = deadTree;
        }
        else if(health.CurrentEnvironment < stagnant)
        {
            treeRenderer.sprite = dyingTree;
        } else 
        {
            treeRenderer.sprite = healthyTree;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnvironment();

    }
}
