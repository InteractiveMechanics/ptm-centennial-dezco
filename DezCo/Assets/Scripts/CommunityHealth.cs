using System.Collections;
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

    public int StartHappiness;
    public int StartEnvironment;
    public int StartBudget;
    public int EnvironmentThreshold;
    public int BudgetThreshold;
    public int AdjustedHappiness;
    public int MaxPopulation;
    public int CommentThreshold;
    public string[] TileTags;

    public Slider happinessbar;
    public Slider environmentbar;
    public Slider budgetbar;
    public GameObject Pothole;
    public GameObject board;
    public GameObject[] grass;

    public GameObject commentObject;
    public Transform uiCanvas;

    public IsoWorld isoWorld;

    public int updatePercent;





    // Use this for initialization
    void Start()
    {
        
        MaxValue = 100;
        resetHealth();

        happinessbar.maxValue = MaxValue;

        environmentbar.maxValue = MaxValue;
        budgetbar.maxValue = MaxValue;
        happinessbar.value = MaxValue;
        environmentbar.value = MaxValue;
        budgetbar.value = MaxValue;
        InvokeRepeating("Sadness", 1, 1);
        InvokeRepeating("GenerateRandomComment", 5, 5);
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

    void resetHealth(){
        CurrentHappiness = StartHappiness;
        CurrentEnvironment = StartEnvironment;
        CurrentBudget = StartBudget;
    }

    void UpdatePeople()
    {
        Person[] persons = board.GetComponent<BoardManager>().persons;
        if (CurrentHappiness < previousHappiness){
            for (int i = 0; i < persons.Length; i++)
            {
                Debug.Log(persons[i]);
                persons[i].GetComponent<Person>().Sad();

            }
        } else if (CurrentHappiness > previousHappiness){
            for (int i = 0; i < persons.Length; i++)
            {
                Debug.Log(persons[i]);
                persons[i].GetComponent<Person>().Happy();
            }
        }

    }

    void GenerateRandomComment()
    {


        List<string> comments = new List<string>();
        Person[] persons = board.GetComponent<BoardManager>().persons;

        var healthObjects = FindObjectsOfType<ModifyHealth>();
        for (int i = 0; i < TileTags.Length; i++){
            GameObject[] TileObjects = GameObject.FindGameObjectsWithTag(TileTags[i]);
            if (TileObjects.Length>= 3){
                comments.Add("I wanted to walk in the park, but everything is too crowded.");
            }
        }


        if (CurrentHappiness > CommentThreshold)
        {
            comments.Add("I love our community! We can all have fun and stay healthy.");
        }
        else 
        {
            comments.Add("People seem so grumpy.Maybe they don't like the changes.");
        }

        if (CurrentEnvironment > CommentThreshold)
        {
            comments.Add("What a beautiful day! The trees are blooming and the air is clean.");
        }
        else 
        {
            comments.Add("Our trees don't look so green anymore. What should we do?");
        }

        if (CurrentBudget > CommentThreshold)
        {
            comments.Add("This is a great place to live! We've got lots of good ideas.");
        }
        else 
        {
            comments.Add("Our city is losing money on construction. What should we do?");
        }


        //pull random comment, instantiate a comment bubble, and replace text and place above a random person

        if (comments.Count > 0)
        {
            

            string randomComment = comments[Random.Range(0, comments.Count)];
            Person[] allPersons = board.GetComponent<BoardManager>().persons;
            Debug.Log(allPersons);
            Person randomPerson = allPersons[Random.Range(0, persons.Length)];
            IsoObject iso = randomPerson.GetComponent<IsoObject>();
            GameObject RandomCommentModal = Instantiate(commentObject, uiCanvas);
            RandomCommentModal.gameObject.GetComponentInChildren<Text>().text = randomComment;


            Vector2 commentLocation2D = isoWorld.IsoToScreen(iso.position);
            //not sure why, but misregistered
            Vector2 adjustedCommentLocation = new Vector2(commentLocation2D.x - 1920f, commentLocation2D.y - 1080f);

            RandomCommentModal.gameObject.GetComponent<RectTransform>().anchoredPosition = adjustedCommentLocation;

        }


    }




    void UpdateGrass()
    {
        for (int i = 0; i < grass.Length; i++)
        {
            //Debug.Log(grass[i]);
            grass[i].GetComponent<grassHealth>().CheckEnvironment();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        CalculateHealth();
        AdjustPopulation();
        //Debug.Log(previousHappiness+", "+CurrentHappiness);
        if (previousEnvironment!=CurrentEnvironment) {
            UpdateGrass();
        }

        //was used for updating on results
        //UpdateRandomPeople();
          
    }

    public void CalculateHealth(){
        previousHappiness = CurrentHappiness;
        previousEnvironment = CurrentEnvironment;
        previousBudget = CurrentBudget;

        //might not be true?
        resetHealth();
        
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

    public void UnhealthyBudget(){
        StartBudget =  StartBudget - 20 ;
    }
	
    public void UnhealthyEnvironment()
    {
        StartEnvironment = StartEnvironment - 20;
    }

    public void UnhealthyHappiness()
    {
        StartHappiness = StartHappiness - 20;
    }
	
}
