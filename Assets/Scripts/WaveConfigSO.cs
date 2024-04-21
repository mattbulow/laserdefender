using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]

public class WaveConfigSO : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Vector2> waypoints;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 0.7f;
    [SerializeField] float spawnTimeVariance = 0.1f;
    [SerializeField] float minimumSpawnTime = 0.2f;
    [SerializeField][Range(1, 20)] int enemyCountMin;
    [SerializeField][Range(1, 20)] int enemyCountMax;


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
        if (enemyCountMin > enemyCountMax)
        {
            int temp = enemyCountMin;
            enemyCountMin = enemyCountMax;
            enemyCountMax = temp;
        }
        return Random.Range(enemyCountMin,enemyCountMax+1);
    }

    public GameObject GetEnemyPrefab() 
    {
        return enemyPrefab;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns-spawnTimeVariance,
                                       timeBetweenEnemySpawns+spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

}
