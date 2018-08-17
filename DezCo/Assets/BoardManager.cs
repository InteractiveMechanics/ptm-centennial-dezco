using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Assets.UltimateIsometricToolkit.Scripts.Core;



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
    public GameObject prompt;
    public GameObject[] groundTiles;
    public GameObject[] treeTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList(){
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++){
            for (int z = 1; z < rows - 1; z++)
            {
                gridPositions.Add(new Vector3(x, 0f, z));
            }
        }
    }

    void BoardSetup (){
        boardHolder = new GameObject("Board").transform;


        for (int x = -1; x < columns+1; x++)
        {
            for (int z = -1; z < rows+1; z++)
            {
                GameObject toInstantiate = groundTiles[Random.Range(0, groundTiles.Length)];

                if (x == -1 || x == columns || z == -1 || z == rows)
                    toInstantiate = outerWallTiles[Random.Range (0, outerWallTiles.Length)];
                
                GameObject instance = Instantiate(toInstantiate, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
                IsoTransform iso = instance.AddComponent<IsoTransform>();
                iso.Position = new Vector3(x, 0f, z);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 RandomPosition(){
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum){
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i< objectCount; i++){
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Debug.Log(tileChoice);

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            GameObject randomTile = Instantiate(tileChoice, new Vector3(0f, 0f, 0f), Quaternion.identity);
            randomTile.name = "tree" + i;
            IsoTransform iso = randomTile.AddComponent<IsoTransform>();
            iso.Position = randomPosition;
        }
    }

    public void SetupScene(){
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(treeTiles, 3, 3);
        //LayoutObjectAtRandom(treeTiles, treeCount.minimum, treeCount.maximum);
        //Instantiate(prompt, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }
}
