using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityHealth : MonoBehaviour {
    public float CurrentHappiness { get; set; }
    public float MaxValue { get; set; }

    public Slider happinessbar;

    // Use this for initialization
    void Start()
    {
        MaxValue = 20f;
        CurrentHappiness = MaxValue;

        happinessbar.value = CalculateValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
            AdjustHappiness(6);
    }

    float CalculateValue(){
        return CurrentHappiness / MaxValue;
    }

    void AdjustHappiness(float happinessAmount){
        CurrentHappiness -= happinessAmount;
        if(CurrentHappiness<=0){
            Die();
        }
    }

    void Die(){
        CurrentHappiness = 0;
        Debug.Log("dead");
    }
	
	
}
