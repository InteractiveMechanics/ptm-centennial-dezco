﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataController : MonoBehaviour
{
    public GameData allData;

    private string gameDataFileName = "data.json";

    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        LoadGameData();

        //LoadPlayerProgress();

        //SceneManager.LoadScene("MenuScreen");
    }


    public GameData GetRandomScenario(){
        return allData;
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            //Debug.Log(loadedData.pucks[0].name);
            // Retrieve the allRoundData property of loadedData
            allData = loadedData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

}