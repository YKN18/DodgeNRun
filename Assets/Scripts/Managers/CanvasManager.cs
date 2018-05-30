using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager instance;

    [SerializeField] GameObject pausePanel, pauseButton, countDownPanel;
    [SerializeField] Text timeText, healthText, coinText, countDownText;
    private bool isPaused = false;

    void Awake()
    {
        instance = this;
    }


    public void Pause() {
        //Function triggered by the pause button, activates the pause panel and
        //stops time and soundrack
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        SoundManager.instance.PauseSoundtrack();
    }

    public void StartCountDown() {
        pausePanel.SetActive(false);
        countDownPanel.SetActive(true);
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown() {
        //Time.deltaTime can't be uses sice the timescale is set to 0
        float cur = Time.realtimeSinceStartup;
        float t = 4;
        countDownText.text = ((int)t).ToString();
        while (t > 1.05) {
            t -= Time.realtimeSinceStartup - cur;
            cur = Time.realtimeSinceStartup;
            countDownText.text = ((int)t).ToString();
            yield return 0;
        }
        Resume();
    }

    public void Resume() {
        //Resumes the game from pause
        countDownPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        SoundManager.instance.PlaySoundtrack();
    }

    public void SetHealth(int v)
    {
        //Sets the health indicator to the current health
        healthText.text = v.ToString() + "/5";
    }

    public void SetTime(float s)
    {
        //Sets the clock to the current time
        timeText.text = ((int)s).ToString();
    }

    public void SetCoins(int c)
    {
        //Sets the coins indicator to the current coin count
        coinText.text = c.ToString();
    }

}
