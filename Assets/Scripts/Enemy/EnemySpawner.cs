using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveObj;
    [SerializeField] float timeBetweenWaves = 0f;

    PathGenerator pathGenerator;

    private void Awake()
    {
        pathGenerator = GetComponent<PathGenerator>();
        if (pathGenerator == null) { Debug.Log("pathGenerator is NULL"); }
    }
    public void SetWaypoints(List<Vector2> waypoints)
    {
        if (waveObj != null)
        {
            waveObj.SetWaypoints(waypoints);
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return waveObj;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {
        while (true) 
        {
            pathGenerator.UpdateWaveWithWaypoints();
            for (int n = 0; n < waveObj.GetEnemyCount(); ++n)
            {
                Instantiate(waveObj.GetEnemyPrefab(n),
                            waveObj.GetStartingWaypoint(),
                            Quaternion.identity,
                            transform);
                yield return new WaitForSeconds(waveObj.GetRandomSpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }


    }

}
