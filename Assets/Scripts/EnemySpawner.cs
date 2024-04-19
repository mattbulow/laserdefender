using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        while (true) 
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int n = 0; n < currentWave.GetEnemyCount(); ++n)
                {
                    Instantiate(currentWave.GetEnemyPrefab(n),
                                currentWave.GetStartingWaypoint().position,
                                Quaternion.identity,
                                transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }


    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
