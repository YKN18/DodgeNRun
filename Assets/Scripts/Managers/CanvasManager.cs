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
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        SoundManager.instance.PauseSoundtrack();
    }

    public void Resume() {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        SoundManager.instance.PlaySoundtrack();
    }

    public void SetHealth(int v)
    {
        healthText.text = v.ToString() + "/5";
    }

    public void SetTime(float s)
    {
        timeText.text = ((int)s).ToString();
    }

    public void SetCoins(int c)
    {
        coinText.text = c.ToString();
    }

}
