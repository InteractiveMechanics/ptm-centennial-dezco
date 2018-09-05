using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IsoTools;

public class CommunityHealth : MonoBehaviour {
    public int CurrentHappiness { get; set; }
    public int CurrentEnvironment { get; set; }
    public int CurrentBudget { get; set; }
    public int MaxValue { get; set; }
    public int EnvironmentThreshold { get; set; }
    public int BudgetThreshold { get; set; }

    public Slider happinessbar;
    public Slider environmentbar;
    public Slider budgetbar;
    public GameObject Pothole;
    public GameObject board;




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

    }

    // Update is called once per frame
    void Update()
    {
        CalculateHealth();


        //add trash
        //update trees
        //update grass


        //if (Input.GetKey(KeyCode.X)){
        //    CalculateHealth();
        //}
            
    }

    float CalculateValue(int value){
        return value / MaxValue;
    }

    public void CalculateHealth(){

        int PreviousHappiness = CurrentHappiness;

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

        if (CurrentEnvironment < EnvironmentThreshold || CurrentBudget < BudgetThreshold)
        {
            PreviousHappiness --;
            CurrentHappiness = PreviousHappiness;
        }

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


    //public void TallyHealth(int happinessAmount, int environmentAmount, int budgetAmount){
    //    CurrentHappiness += happinessAmount;
    //    CurrentEnvironment += environmentAmount;
    //    CurrentBudget += budgetAmount;
    //    happinessbar.value = CalculateValue();
    //    if(CurrentHappiness<=0){
    //        Die();
    //    }
    //}

    void Die(){
        CurrentHappiness = 0;
        Debug.Log("dead");
    }
	
	
}
