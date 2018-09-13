using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pothole : MonoBehaviour
{

    public GameManager game;
    public CommunityHealth health;
    public int budgetThreshold;
    public GameObject childObject;

    // Use this for initialization
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        health = game.GetComponent<CommunityHealth>();

    }

    void CheckBudget()
    {
        if (health.CurrentBudget < budgetThreshold)
        {
            childObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckBudget();

    }
}