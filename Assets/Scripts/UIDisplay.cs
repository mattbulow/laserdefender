using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Health playerHealth;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    private void Update()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        healthSlider.value = Mathf.Clamp(playerHealth.GetHealth(), 0, 999999999);
    }

}
