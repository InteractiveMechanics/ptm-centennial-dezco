﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IsoTools;

public class CommunityHealth : MonoBehaviour {
    public int CurrentHappiness { get; set; }
    public int CurrentEnvironment { get; set; }
    public int CurrentBudget { get; set; }
    public int previousHappiness { get; set; }
    public int previousEnvironment { get; set; }
    public int previousBudget { get; set; }
    public int MaxValue { get; set; }
    public int EnvironmentThreshold;
    public int BudgetThreshold;
    public int AdjustedHappiness;
    public int MaxPopulation;

    public Slider happinessbar;
    public Slider environmentbar;
    public Slider budgetbar;
    public GameObject Pothole;
    public GameObject board;
    public GameObject[] grass;





    // Use this for initialization
    void Start()
    {
        
        MaxValue = 100;
        CurrentHappiness = MaxValue;
        CurrentEnvironment = MaxValue;
        CurrentBudget = MaxValue;

        happinessbar.maxValue = MaxValue;

        environmentbar.maxValue = MaxValue;
        budgetbar.maxValue = MaxValue;
        happinessbar.value = MaxValue;
        environmentbar.value = MaxValue;
        budgetbar.value = MaxValue;
        InvokeRepeating("Sadness", 1, 1);

    }

    void Sadness(){
        if (CurrentEnvironment < EnvironmentThreshold || CurrentBudget < BudgetThreshold){
            Debug.Log("I'm sad");
            AdjustedHappiness--;
        } else {
            AdjustedHappiness = 0;
        }
    }

    void AdjustPopulation(){
        
        float happiness = CurrentHappiness;
        float max = MaxValue;
        float maxPop = MaxPopulation;
        board.GetComponent<BoardManager>().population = Mathf.RoundToInt(maxPop * (happiness / max));
    }

    void UpdateGrass(){
        for (int i = 0; i < grass.Length; i++){
            //Debug.Log(grass[i]);
            grass[i].GetComponent<grassHealth>().CheckEnvironment();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        CalculateHealth();
        AdjustPopulation();
        Debug.Log(previousHappiness+", "+CurrentHappiness);
        if (previousHappiness!=CurrentHappiness) {
            UpdateGrass();
        }
            
    }

    public void CalculateHealth(){
        previousHappiness = CurrentHappiness;
        previousEnvironment = CurrentEnvironment;
        previousBudget = CurrentBudget;
        CurrentHappiness = MaxValue;
        CurrentEnvironment = MaxValue;
        CurrentBudget = MaxValue;
        
        var healthObjects = FindObjectsOfType<ModifyHealth>();
        for (int i = 0; i < healthObjects.Length; i++)
        {
            var healthObject = healthObjects[i].GetComponent<ModifyHealth>(); //get all gameobjects with health values attached
            //Debug.Log(healthObject.environment);
            CurrentHappiness += healthObject.happiness; //adjust happiness from base value
            CurrentEnvironment += healthObject.environment; //adjust happiness from base value
            CurrentBudget += healthObject.budget; //adjust happiness from base value

        }

        CurrentHappiness = CurrentHappiness + AdjustedHappiness;
        happinessbar.value = CurrentHappiness; //change sliders
        environmentbar.value = CurrentEnvironment;
        budgetbar.value = CurrentBudget;
    }

    public void ShowDisrepair(){
        Transform boardHolder = board.transform;
        if (CurrentBudget < BudgetThreshold) {
            GameObject pothole = Instantiate(Pothole, new Vector3(0f, 0f, 0f), Quaternion.identity);
            IsoObject iso = pothole.GetComponent<IsoObject>();
            iso.position = new Vector3(21f,24f,0f);
            pothole.transform.SetParent(boardHolder);
        }
    }

    void Die(){
        CurrentHappiness = 0;
        Debug.Log("dead");
    }
	
	
}
