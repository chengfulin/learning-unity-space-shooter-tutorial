using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private float asteroidSpeedUpUnit = -0.2f;


    private void Start()
    {
        initialize();
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (gameOver)
        {
            restartText.text = $"{scoreText.text}\nClick to restart";
            StopAllCoroutines();
            restart = true;
        }
        if (restart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

   public void GameOver()
    {
        gameOverText.text = "Game Over";
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
            UpgradeHazards(asteroid01, waveIndex -1);
            UpgradeHazards(asteroid02, waveIndex -1);
            UpgradeHazards(asteroid03, waveIndex -1);

            for (int i = 0; i < instantiationQueue.Count; ++i)
            {
                switch (instantiationQueue[ Random.Range(0, instantiationQueue.Count) ])
                {
                    case 3:
                        InstantiateAsteroid(asteroid03, new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z));
                        break;
                    case 2:
                        InstantiateAsteroid(asteroid02, new Vector3(spawnValues.x, spawnValues.y, Random.Range(0, spawnValues.z)));
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
        }
    }

    private void UpgradeHazards(GameObject hazard, int waveIndex)
    {
        var hazardMover = hazard.GetComponent<Mover>();
        if (hazardMover != null)
        {
            hazardMover.SpeedUp(waveIndex * asteroidSpeedUpUnit);
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
