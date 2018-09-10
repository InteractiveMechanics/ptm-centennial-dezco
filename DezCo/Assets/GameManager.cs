using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public BoardManager boardScript;

    //for refactoring 
    //private DataController dataController;
    //public GameData data;

	// Use this for initialization
	void Awake () {
        boardScript = GetComponent<BoardManager>();
        InitGame();
	}

    void InitGame(){

        //for refactoring as above
        //dataController = FindObjectOfType<DataController>();
        //data = dataController.GetCurrentData();
        boardScript.SetupScene();
        //boardScript.Populate();
    }

    // Update is called once per frame
    void Update () {
        boardScript.UpdatePopulation();
	}
}
