using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
//using Assets.UltimateIsometricToolkit.Scripts.Core;
using IsoTools;



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
    public GameObject board;
    private IsoWorld isoWorld;
    private List<Vector3> gridPositions = new List<Vector3>();

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
        boardHolder = board.transform;

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
            IsoObject iso = randomTile.GetComponent<IsoObject>();
            iso.position = randomPosition;
            randomTile.transform.SetParent(boardHolder);
            //iso.Size = new Vector3(1, 1, 1);
        }
    }

    void DestroyAroundPos(int x, int y, int radius)
    {
         
    }

    public void SetupScene(){
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(treeTiles, treeCount.minimum, treeCount.maximum);
        GameObject gardenTile = Instantiate(prompt, new Vector3(columns/2, rows/2, 0f), Quaternion.identity);
        IsoObject iso = gardenTile.GetComponent<IsoObject>();
        iso.position = new Vector3(columns/2, rows/2, 0f);
        gardenTile.transform.SetParent(boardHolder);

        //check if other tiles are in this position


    }
}
