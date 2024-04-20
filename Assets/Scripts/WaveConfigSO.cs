using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]

public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Vector2> waypoints;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 0.7f;
    [SerializeField] float spawnTimeVariance = 0.1f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    public void SetWaypoints(List<Vector2> wp) { waypoints = wp; }

    public Vector2 GetStartingWaypoint()
    {
        return waypoints[0];
    }

    public List<Vector2> GetWaypoints()
    {
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    
    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index) 
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns-spawnTimeVariance,
                                       timeBetweenEnemySpawns+spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

}
