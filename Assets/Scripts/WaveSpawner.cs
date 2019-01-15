using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBtwSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBtwWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;
    private bool finishedSpawning;
    public Text waveText; 

    [Header("Boss Configuration")]
    public GameObject boss;
    public Transform bossSpawnPoint;
    public GameObject bossHealthBar;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private void Update()
    {
        if (finishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;

            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            } else
            {
                waveText.text = "Boss Fight!";
                bossHealthBar.SetActive(true);
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
            }
        }
    }

    IEnumerator StartNextWave(int index)
    {
        waveText.text = "Starting...";
        yield return new WaitForSeconds(timeBtwWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        waveText.text = $"Wave {index + 1}/{waves.Length}";
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if (i == currentWave.count - 1)
            {
                finishedSpawning = true;
            } else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBtwSpawns);
        }
    }
}
