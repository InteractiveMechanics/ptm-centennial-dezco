using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
//using Assets.UltimateIsometricToolkit.Scripts.Core;
using IsoTools;
using IsoTools.Internal;



public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count {
        public int minimum;
        public int maximum;

        public Count (int min, int max) {
            minimum = min;
            maximum = max;
        }
    }

    //needs to be refactored into the gamemanager
    private DataController dataController;

    public int columns = 20;
    public int rows = 20;
    public Count treeCount = new Count(5, 9);
    public GameObject[] groundTiles;
    public GameObject[] treeTiles;
    public GameObject[] outerWallTiles;
    public GameObject Person;
    public GameObject[] prompts;
    public int population;
    public int maxOpenPrompts;
    public float promptGenTime;

    private Transform boardHolder;
    private Transform canvas;
    public GameObject board;
    private IsoWorld isoWorld;
    private List<Vector3> gridPositions = new List<Vector3>();
    public Person[] persons;





    private PromptLocation[] promptLocations;
    private List<PromptLocation> occupiedLocations = new List<PromptLocation>();
    private List<PromptLocation> promptsWaiting = new List<PromptLocation>();
    private List<PromptLocation> openLocations = new List<PromptLocation>();

    public int maxClosedPrompts;

    void InitializeList(){
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++){
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup (){
        // in setup func
        //boardHolder = board.transform;

        for (int x = -1; x < columns+1; x++)
        {
            for (int y = -1; y < rows+1; y++)
            {
                GameObject toInstantiate = groundTiles[Random.Range(0, groundTiles.Length)];

                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range (0, outerWallTiles.Length)];
                
                GameObject instance = Instantiate(toInstantiate, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
                IsoObject iso = instance.GetComponent<IsoObject>();
                iso.position = new Vector3(x, y, 0f);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomGridPosition(){
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum){
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i< objectCount; i++){
            Vector3 randomPosition = RandomGridPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            //Debug.Log(tileChoice);
            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            GameObject randomTile = Instantiate(tileChoice, new Vector3(0f, 0f, 0f), Quaternion.identity);
            randomTile.name = "tree" + i;
            IsoObject iso = randomTile.GetComponent<IsoObject>();
            iso.position = randomPosition;
            randomTile.transform.SetParent(boardHolder);
            //iso.Size = new Vector3(1, 1, 1);
        }
    }

    Vector3 RandomPosition()
    {
        float randomX = Random.Range(0f, 44f);
        float randomY = Random.Range(-22, 22f);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);
        return randomPosition;
    }

    public void Populate()
    {
        Debug.Log("populating");
        //boardHolder = board.transform;
        for (int i = 0; i < population; i++)
        {
            InstantiatePerson();
        }
    }

    public void InstantiatePerson()
    {
        boardHolder = board.transform;
        Vector3 randomPosition = RandomPosition();
        GameObject randomPerson = Instantiate(Person, new Vector3(0f, 0f, 0f), Quaternion.identity);
        IsoObject iso = randomPerson.GetComponent<IsoObject>();
        iso.position = randomPosition;
        randomPerson.transform.SetParent(boardHolder);
        randomPerson.GetComponent<Person>().health = this.gameObject.GetComponent<CommunityHealth>();
    }

    public void UpdatePopulation()
    {
        persons = FindObjectsOfType<Person>();
        //Debug.Log(persons.Length);
        if (persons.Length < population) {
            InstantiatePerson();
        } else if ( persons.Length > population)
        {
            Destroy(persons[persons.Length-1].gameObject);
        }    
    }

    public void ActivateRandomPrompt()
    {
        //update lists
        GetLocationLists();

        //Transform _detach = _myList[Random.Range(0, _myList.Count)];

        GameObject Prompt = prompts[Random.Range(0, prompts.Length)];


        //freeplay means all locations are open
        if (promptsWaiting.Count < maxOpenPrompts){
            PromptLocation randomLocation = openLocations[Random.Range(0, openLocations.Count)];
            IsoObject iso = randomLocation.GetComponent<IsoObject>();
            Vector2 promptLocation2D = isoWorld.IsoToScreen(iso.position);
            //Debug.Log("iso to screen: "+promptLocation2D);
            GameObject promptModal = Instantiate(Prompt);
            promptModal.transform.SetParent(canvas, false);
            promptModal.gameObject.GetComponent<Prompt>().promptLocation = randomLocation;
            //Debug.Log(promptModal.gameObject.GetComponent<Prompt>().promptLocation);
            promptModal.gameObject.GetComponent<RectTransform>().anchoredPosition = promptLocation2D;
            randomLocation.GetComponent<PromptLocation>().prompt = promptModal.GetComponent<Prompt>();
            //promptModal.GetComponent<Prompt>().detailsText = randomScenario.details;

        }
    }

    public void ActivateAllPrompts()
    {
        promptLocations = FindObjectsOfType<PromptLocation>();

        GameObject Prompt = prompts[0];

        for (int i = 0; i < promptLocations.Length; i++)
        {
            IsoObject iso = promptLocations[i].GetComponent<IsoObject>();
            Vector2 promptLocation2D = isoWorld.IsoToScreen(iso.position);
            //Debug.Log("iso to screen: "+promptLocation2D);
            GameObject promptModal = Instantiate(Prompt);
            promptModal.transform.SetParent(canvas, false);
            promptModal.gameObject.GetComponent<Prompt>().promptLocation = promptLocations[i];
            //Debug.Log(promptModal.gameObject.GetComponent<Prompt>().promptLocation);
            promptModal.gameObject.GetComponent<RectTransform>().anchoredPosition = promptLocation2D;
            promptLocations[i].GetComponent<PromptLocation>().prompt = promptModal.GetComponent<Prompt>();
            //promptModal.GetComponent<Prompt>().detailsText = randomScenario.details;

        }
    }



    public void GetLocationLists(){
        promptLocations = FindObjectsOfType<PromptLocation>();
        occupiedLocations.Clear();
        promptsWaiting.Clear();
        openLocations.Clear();
        for (int i = 0; i < promptLocations.Length; i++)
        {
            if (promptLocations[i].GetComponent<PromptLocation>().prompt)
            {
                occupiedLocations.Add(promptLocations[i]);
                if (promptLocations[i].GetComponent<PromptLocation>().locationOpen){
                    promptsWaiting.Add(promptLocations[i]);

                }
            } else {
                openLocations.Add(promptLocations[i]);
            }

        }
    }

    public void ClearPrompts(){
        
    }

    public void CheckAndActivatePrompts(){
        
        InvokeRepeating("ActivateRandomPrompt", promptGenTime, 1);
    }



    public void SetupScene(){
        // init steps

        dataController = FindObjectOfType<DataController>();

        boardHolder = board.transform;
        isoWorld = FindObjectOfType<IsoWorld>();
        //Debug.Log(isoWorld);
        canvas = GameObject.Find("uiCanvas").transform;   
        Populate();


        //removing for free play
        //CheckAndActivatePrompts();
        ActivateAllPrompts();
    }
}
