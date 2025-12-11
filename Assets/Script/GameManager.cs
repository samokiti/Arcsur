using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {

        if (UIController.Instance.gameover.activeSelf) return;

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            pause();
        }
    }
    public void TriggerGameOver()
    {
        StartCoroutine(GameoverRoutine());
    }

    IEnumerator GameoverRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (UIController.Instance.gameover != null)
        {
            UIController.Instance.gameover.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
    public void pause()
    {
        if (UIController.Instance.pausepanel.activeSelf == false)
        {
            UIController.Instance.pausepanel.SetActive(true);
            Time.timeScale = 0f; 
        }
        else
        {
            UIController.Instance.pausepanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
