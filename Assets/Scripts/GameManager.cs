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

    [SerializeField] GameObject GameOverPanel, PausePanel;

    [SerializeField] AudioSource TiresAudio, EngineAudio;

    private Rigidbody Car_RB;

    public bool isDrifting;

    private float driftPoints = 0f;

    private float levelTimer = 120f;

    void Start()
    {
        Car_RB = GameObject.FindGameObjectWithTag("Car").GetComponent<Rigidbody>();

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                PauseGame();
            }
            else if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
        }

        Debug.Log(Car_RB.velocity.magnitude);
        if (Car_RB.velocity.magnitude > 3 && !EngineAudio.isPlaying)
        {
            EngineAudio.Play();
        }
        else if (Car_RB.velocity.magnitude <= 3 && EngineAudio.isPlaying)
        {
            EngineAudio.Stop();
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
            TiresSoundPlay();

            driftText.gameObject.SetActive(true);
            driftPoints += Time.deltaTime * 100f;  
            driftPointText.text = Math.Round(driftPoints, 2).ToString();  

            yield return null;  
        }
        yield return new WaitForSeconds(0.5f);
        driftText.gameObject.SetActive(false);
        TiresAudio.Stop();
        StartCoroutine(IsDriftingRoutine());
    }

    public void TiresSoundPlay()
    {
        if (!TiresAudio.isPlaying)
        {
            new WaitForSeconds(0.25f);
            TiresAudio.Play();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        PausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PausePanel.SetActive(false);
    }
}
