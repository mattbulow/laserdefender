using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;

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
        score += amount;
        return score;
    }
    public void ResetScore()
    {
        score = 0;
    }
}
