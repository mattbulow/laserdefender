using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float gameOverSceneDelay = 2f;
    ScoreKeeper scoreKeeper;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        scoreKeeper.ResetScore();
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOverMenu", gameOverSceneDelay));
    }

    IEnumerator WaitAndLoad(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application.Quit has run.");
    }

}
