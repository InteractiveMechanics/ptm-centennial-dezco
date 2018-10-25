using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slidercolor : MonoBehaviour {
 
     private Slider slider;
     private int counter;
 
     //public int MaxHealth = 100;
     public Color MaxHealthColor = Color.green;
     public Color MedHealthColor = Color.yellow;
     public Color MinHealthColor = Color.red;
    public Sprite MaxHealthImage;
    public Sprite MedHealthImage;
    public Sprite MinHealthImage;
    public GameObject HealthIcon;
    public GameObject SliderFill;
 
     private void Awake() {
         slider = gameObject.GetComponent<Slider>();
         //counter = MaxHealth;            // just for testing purposes
     }
 
     private void Start() {
         slider.wholeNumbers = true;        // I dont want 3.543 Health but 3 or 4
         slider.minValue = 0f;
         //slider.maxValue = MaxHealth;
         //slider.value = MaxHealth;        // start with full health
     }
 
     private void Update() {
        SliderFill.GetComponent<Image>().color = MinHealthColor;
        HealthIcon.GetComponent<Image>().sprite = MinHealthImage;
        if (slider.value > 30)
        {
            SliderFill.GetComponent<Image>().color = MedHealthColor;
            HealthIcon.GetComponent<Image>().sprite = MedHealthImage;
        }
        if (slider.value > 70) {
            SliderFill.GetComponent<Image>().color = MaxHealthColor;
            HealthIcon.GetComponent<Image>().sprite = MaxHealthImage;
        }

     }
 
     public void UpdateHealthBar(int val) {
         slider.value = val;
         //Fill.color = Color.Lerp(MinHealthColor, MaxHealthColor, (float)val / MaxHealth);
     }
}
