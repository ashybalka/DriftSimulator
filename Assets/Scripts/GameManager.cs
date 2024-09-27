using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text driftText;
    [SerializeField] TMP_Text driftPointText;
    [SerializeField] TMP_Text levelTimerText;
    [SerializeField] GameObject GameOverPanel;

    public bool isDrifting;

    private float driftPoints = 0f;

    private float levelTimer = 120f;

    void Start()
    {
        Time.timeScale = 1f;
        GameOverPanel.SetActive(false);
        driftText.gameObject.SetActive(false);
        StartCoroutine(IsDriftingRoutine());
    }

    void Update()
    {
        levelTimer -= Time.deltaTime;
        levelTimerText.text = TimeSpan.FromSeconds(levelTimer).ToString("mm':'ss");

        if (levelTimer < 0f)
        {
            GameOver();
        }

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void AgainButton()
    {
        SceneManager.LoadScene(1);
    }

    public void AdButton()
    {
        driftPoints *= 2f;
    }

    IEnumerator IsDriftingRoutine()
    {
        while (isDrifting)
        {
            driftText.gameObject.SetActive(true);

            driftPoints += Time.deltaTime * 100f;  
            driftPointText.text = System.Math.Round(driftPoints, 2).ToString();  

            yield return null;  
        }
        yield return new WaitForSeconds(0.5f);
        driftText.gameObject.SetActive(false);

        StartCoroutine(IsDriftingRoutine());
    }
}
