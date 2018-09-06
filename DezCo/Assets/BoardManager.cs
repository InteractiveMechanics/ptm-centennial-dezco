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

    public int columns = 20;
    public int rows = 20;
    public Count treeCount = new Count(5, 9);
    public GameObject[] groundTiles;
    public GameObject[] treeTiles;
    public GameObject[] outerWallTiles;
    public GameObject Person;
    public GameObject Prompt;
    public int population;

    private Transform boardHolder;
    private Transform canvas;
    public GameObject board;
    private IsoWorld isoWorld;
    private List<Vector3> gridPositions = new List<Vector3>();
    private Person[] persons;
    private PromptLocation[] promptLocations;
    public int promptOffset;

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
        float randomX = Random.Range(0f, (float)columns);
        float randomY = Random.Range(0f, (float)rows);
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
    }

    public void UpdatePopulation()
    {
        persons = FindObjectsOfType<Person>();
        //Debug.Log(persons.Length);
        if (persons.Length < population) {
            InstantiatePerson();
        } else if ( persons.Length > population)
        {
            Destroy(persons[persons.Length - 1].gameObject);
        }    
    }

    public void ActivateRandomPrompt()
    {
        promptLocations = FindObjectsOfType<PromptLocation>();
        if (promptLocations.Length > 0){
            PromptLocation randomLocation = promptLocations[Random.Range(0, promptLocations.Length - 1)];
            IsoObject iso = randomLocation.GetComponent<IsoObject>();
            Color tmp = randomLocation.GetComponentInChildren<SpriteRenderer>().color;
            tmp.a = 1f;
            randomLocation.GetComponentInChildren<SpriteRenderer>().color = tmp;
            Vector2 promptLocation2D = isoWorld.IsoToScreen(iso.position);
            GameObject promptModal = Instantiate(Prompt, promptLocation2D, Quaternion.identity);
            promptModal.transform.SetParent(canvas, false);
            promptLocation2D = new Vector2(promptLocation2D.x, promptLocation2D.y + promptOffset);
            promptModal.transform.position = promptLocation2D;
        }



    }


    public void SetupScene(){
        // init steps
        boardHolder = board.transform;
        isoWorld = FindObjectOfType<IsoWorld>();
        canvas = FindObjectOfType<Canvas>().transform;   
        Populate();
        ActivateRandomPrompt();



        //check if other tiles are in this position


    }
}
