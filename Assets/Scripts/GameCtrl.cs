﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour
{
    [SerializeField] private GameObject asteroid01 = null;
    [SerializeField] private GameObject asteroid02 = null;
    [SerializeField] private GameObject asteroid03 = null;
    [SerializeField] private GameObject enemy = null;
    
    [SerializeField] private Vector3 spawnValues = Vector3.zero;
    [SerializeField] private int hazardCount = 10;
    [SerializeField] [Header("Seconds before spawn")] private float spawnWait = 0.5f;
    [SerializeField] [Header("Seconds before start")] private float startWait = 1f;
    [SerializeField] [Header("Seconds before wave")] private float waveWait = 5f;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text restartText;
    [SerializeField] private Text gameOverText;

    private bool gameOver = false;
    private bool restart = false;
    private int score = 0;
    private int waveIndex = 1;
    private IList<int> instantiationQueue = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        initialize();
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ToGameOver()
    {
        this.gameOver = true;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void initialize()
    {
        this.scoreText.text = "";
        this.restartText.text = "";
        this.gameOverText.text = "";
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        var waveHazardCount = hazardCount;
        UpdateInstantiationQueue(waveHazardCount);

        while (true)
        {
            
            Debug.unityLogger.Log($"=== Wave({waveIndex}), hazards={waveHazardCount}");

            for (int i = 0; i < instantiationQueue.Count; ++i)
            {
                switch (instantiationQueue[ Random.Range(0, instantiationQueue.Count) ])
                {
                    case 3:
                        InstantiateAsteroid(asteroid03, new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z));
                        break;
                    case 2:
                        InstantiateAsteroid(asteroid02, new Vector3(spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z)));
                        break;
                    case 1:
                        InstantiateAsteroid(asteroid01, new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z));
                        break;
                }
                yield return new WaitForSeconds(spawnWait);
            }
            
            yield return new WaitForSeconds(waveWait);

            this.waveIndex++;
            var append = waveHazardCount >> 2;
            waveHazardCount += append;
            UpdateInstantiationQueue(append);

            if (gameOver)
            {
                restartText.text = "Click to restart";
                restart = true;
                break;
            }
        }
    }

    private void UpdateInstantiationQueue(int appendSize)
    {
        for (int i = 0; i < appendSize; ++i)
        {
            if ((waveIndex & 3) == 3)
            {
                instantiationQueue.Add(3);
            }
            else if ((waveIndex & 2) == 2)
            {
                instantiationQueue.Add(2);
            }
            else
            {
                instantiationQueue.Add(1);
            }
        }
    }

    private void InstantiateAsteroid(GameObject asteroid, Vector3 position)
    {
        GameObject hazard = asteroid;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazard, position, spawnRotation);
    }
}
