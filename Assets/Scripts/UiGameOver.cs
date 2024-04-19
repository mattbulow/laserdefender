using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        string score = scoreKeeper.GetScore().ToString();
        scoreText.text = "Score:\n" + score;
    }
}
