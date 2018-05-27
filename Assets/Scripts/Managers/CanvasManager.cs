using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager instance;

    [SerializeField] GameObject pausePanel, pauseButton;
    [SerializeField] Text timeText, healthText, coinText;
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

    public void Resume() {
        //Resumes the game from pause
        pausePanel.SetActive(false);
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
