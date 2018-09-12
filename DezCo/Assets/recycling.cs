using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recycling : MonoBehaviour {

    public bool open;
    public GameManager game;
    public CommunityHealth health;
    public int budgetThreshold;
    public Sprite litter;
    public Sprite clean;
    private SpriteRenderer recycleRenderer;



    // Use this for initialization
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        health = game.GetComponent<CommunityHealth>();
        recycleRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();

    }

    void CheckBudget()
    {
        if (health.CurrentBudget < budgetThreshold)
        {
            recycleRenderer.sprite = litter;
        }
        else
        {
            recycleRenderer.sprite = clean;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckBudget();

    }
}
