using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IsoTools;

public class Prompt : MonoBehaviour {

    public Text scenarioName;
    public string type;
    public Text details;
    public Text bestPuck;
    public int[] bestReward;
    public Text bestText;
    public Text elseText;
    public Image characterImage;
    public GameObject completePanel;
    public GameObject resultsPanel;
    public GameObject mainPanel;
    public GameObject incorrectPanel;
    private bool timerEnd = true;
    public float targetTime = 5.0f;
    public Text resultsHappiness;
    public Text resultsEnvironment;
    public Text resultsBudget;

    
    //public string text;
    //public string detailsObject;
    //public string detailsText;
    private IsoWorld isoWorld;
    public PromptLocation promptLocation;



	// Use this for initialization
	void Start () {
        isoWorld = GameObject.FindWithTag("IsoWorld").GetComponent<IsoWorld>();
	}

    public void hideAll(){
        mainPanel.SetActive(false);
        resultsPanel.SetActive(false);
        completePanel.SetActive(false);
        incorrectPanel.SetActive(false);
    }

    public void showMain()
    {
        mainPanel.SetActive(true);
        resultsPanel.SetActive(false);
        completePanel.SetActive(false);
        incorrectPanel.SetActive(false);
    }

    public void showComplete()
    {
        mainPanel.SetActive(false);
        resultsPanel.SetActive(false);
        completePanel.SetActive(true);
        incorrectPanel.SetActive(false);
    }

    public void showIncorrect()
    {
        mainPanel.SetActive(false);
        resultsPanel.SetActive(false);
        completePanel.SetActive(false);
        incorrectPanel.SetActive(true);
    }

    public void showResults(ModifyHealth health)
    {
        if (health){
            mainPanel.SetActive(false);
            resultsPanel.SetActive(true);
            completePanel.SetActive(false);
            timerEnd = false;
            resultsHappiness.text = health.happiness.ToString();
            resultsEnvironment.text = health.environment.ToString();
            resultsBudget.text = health.budget.ToString();
        }
    }


    void timerEnded(){
        hideAll();
    }


	
	// Update is called once per frame
	void Update () {

        if (!timerEnd)
        {
            //decrease timer after showresults
            targetTime -= Time.deltaTime;
        }
        if (targetTime <= 0.0f)
        {
            timerEnd = true;
            timerEnded();
        }
        
        Vector3 location3d = promptLocation.GetComponent<IsoObject>().position;
        Vector2 promptPosition = isoWorld.IsoToScreen(location3d);
        gameObject.GetComponent<RectTransform>().localPosition = new Vector2 (promptPosition.x- 1920f, promptPosition.y-1080f);
        //detailsText = (gameObject.transform.position.x + ", " + gameObject.transform.position.y);
        //detailsObject.GetComponent<Text>().text = detailsText;
	}
}
