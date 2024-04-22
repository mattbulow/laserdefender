using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int incrementDifficultyThreshold = 10000;
    [SerializeField] private int difficultyLevel = 0;
    private int score = 0;

    EnemySpawner enemySpawner;

    int numOfInstances = 0;
    private void Awake()
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {
        numOfInstances = FindObjectsOfType(GetType()).Length;
        if (numOfInstances > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() { return score; }
    public int IncrementScore(int amount)
    {
        int scorePrevious = score;
        score += amount;
        int threshold = incrementDifficultyThreshold * (difficultyLevel + 1);
        if (score >= threshold && scorePrevious < threshold)
        {
            if (enemySpawner == null)
            {
                enemySpawner = FindObjectOfType<EnemySpawner>();
            }
            ++difficultyLevel;
            enemySpawner.SetDifficulty(difficultyLevel);
        }
        return score;
    }
    public void ResetScore()
    {
        score = 0;
    }
}
