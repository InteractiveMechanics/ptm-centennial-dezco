using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class DataController : MonoBehaviour
{

    public ScenarioList scenarios = new ScenarioList();
    private string gameDataFileName = "scenarios.json";

    void Start()
    {
        LoadGameData();
    }

    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        if (File.Exists(filePath))
        {
            //did not work as json!
            string dataAsJson = File.ReadAllText(filePath);
            scenarios = JsonUtility.FromJson<ScenarioList>(dataAsJson);
            Debug.Log(scenarios);



        }

        //does not work either
        //TextAsset asset = Resources.Load("scenarios.txt") as TextAsset;
        //if (asset != null){
        //    scenarios = JsonUtility.FromJson<ScenarioList>(asset.text);
        //}
        //else
        //{
        //    Debug.LogError("Cannot load game data!");
        //}
    }

}
