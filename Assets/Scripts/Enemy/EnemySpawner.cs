using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waves;
    [SerializeField] float timeBetweenWaves = 0f;

    int difficultyLevel = 0;

    WaveConfigSO currentWave;
    PathGenerator pathGenerator;

    private void Awake()
    {
        pathGenerator = GetComponent<PathGenerator>();
        if (pathGenerator == null) { Debug.Log("pathGenerator is NULL"); }
    }
    public void SetWaypoints(int idx, List<Vector2> waypoints)
    {
        if (waves[idx] != null)
        {
            waves[idx].SetWaypoints(waypoints);
        }
    }

    public void SetDifficulty(int difficulty)
    {
        difficultyLevel = difficulty;
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        while (true) 
        {
            // We will choose which wave to run randomly. Each wave represents a different enemy type
            int waveIdx = UnityEngine.Random.Range(0, Mathf.Clamp(difficultyLevel,0, waves.Count-1)+1);
            currentWave = waves[waveIdx];

            pathGenerator.UpdateWaveWithWaypoints(waveIdx);
            // We will get a random number of enemies to spawn from the wave object
            for (int n = 0; n < waves[waveIdx].GetEnemyCount(); ++n)
            {
                Instantiate(currentWave.GetEnemyPrefab(),
                            currentWave.GetStartingWaypoint(),
                            Quaternion.identity,
                            transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }


    }

}
