using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassHealth : MonoBehaviour
{
    


    public GameManager game;
    public CommunityHealth health;
    public int unhealthyThreshold;
    public int fairThreshold;
    public int moderateThreshold;

    public Sprite unhealthy;
    public Sprite fair;
    public Sprite moderate;
    public Sprite healthy;
    public SpriteRenderer grassRenderer;



    // Use this for initialization
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        health = game.GetComponent<CommunityHealth>();
        //grassRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();

    }

    public void CheckEnvironment()
    {
        Debug.Log("check grass health");
        if (health.CurrentEnvironment < unhealthyThreshold)
        {
            grassRenderer.sprite = unhealthy;
        }
        else if (health.CurrentEnvironment < fairThreshold)
        {
            grassRenderer.sprite = fair;
        }
        else if (health.CurrentEnvironment < moderateThreshold)
        {
            grassRenderer.sprite = moderate;
        }
        else
        {
            grassRenderer.sprite = healthy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
