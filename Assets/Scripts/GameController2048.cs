using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameController2048 : MonoBehaviour
{
    [SerializeField] GameObject fillPrefab;
    [SerializeField] GameObject winningPanel;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] Cell2048[] allCells;
    [SerializeField] Text scoreDisplay;
    [SerializeField] int winningScore;

    public static Action<string> slide;
    public static GameController2048 instance;
    public static int ticker; //count actions received by cells
    public int myScore;
    public Sprite[] fillSprites;

    int isGameOver;
    bool hasWon;
    bool canMove = true;

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //spwan twice at the start
        StartSpawnFill();
        StartSpawnFill();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("up"))
            {
                ticker = 0;
                isGameOver = 0;
                slide("w");
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("left"))
            {
                ticker = 0;
                isGameOver = 0;
                slide("a");
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown("down"))
            {
                ticker = 0;
                isGameOver = 0;
                slide("s");
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown("right"))
            {
                ticker = 0;
                isGameOver = 0;
                slide("d");
            }
        }
    }

    public void SpawnFill()
    {
        bool isFull = true;
        for(int i = 0; i < allCells.Length; ++i)
        {
            if (allCells[i].fill == null)
                isFull = false;
        }

        if(!isFull)
        {
            int spawnPos = UnityEngine.Random.Range(0, allCells.Length);
            //check duplicate spawns
            while (allCells[spawnPos].transform.childCount != 0)
            {
                Debug.Log(allCells[spawnPos].name + " is already filled!");
                spawnPos = UnityEngine.Random.Range(0, allCells.Length); //reassign the value
            }

            float chance = UnityEngine.Random.Range(0f, 1f);
            if (chance >= 0.2f && chance < 0.8f)
            {
                GameObject tempFill = Instantiate(fillPrefab, allCells[spawnPos].transform);
                Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
                tempFillComp.FillValueUpdate(2);
                allCells[spawnPos].GetComponent<Cell2048>().fill = tempFillComp;
            }
            else if (chance >= 0.8f)
            {
                GameObject tempFill = Instantiate(fillPrefab, allCells[spawnPos].transform);
                Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
                tempFillComp.FillValueUpdate(4);
                allCells[spawnPos].GetComponent<Cell2048>().fill = tempFillComp;
            }
        }
        else
            Debug.Log("Full");
    }

    public void StartSpawnFill()
    {
        int spawnPos = UnityEngine.Random.Range(0, allCells.Length);
        //check duplicate spawns
        while (allCells[spawnPos].transform.childCount != 0)
        {
            Debug.Log(allCells[spawnPos].name + " is already filled!");
            spawnPos = UnityEngine.Random.Range(0, allCells.Length); //reassign the value
        }
        GameObject tempFill = Instantiate(fillPrefab, allCells[spawnPos].transform);
        Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
        tempFillComp.FillValueUpdate(2);
        allCells[spawnPos].GetComponent<Cell2048>().fill = tempFillComp;
    }

    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        scoreDisplay.text = myScore.ToString();
    }

    public void WinningCheck(int highestFill)
    {
        if(!hasWon)
        {
            if (highestFill == winningScore)
            {
                winningPanel.SetActive(true);
                hasWon = true;
                canMove = false;
            }
        }
    }

    public void GameOverCheck()
    {
        //check all 16 cells
        ++isGameOver;
        if (isGameOver == 16)
        {
            gameoverPanel.SetActive(true);
            canMove = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
