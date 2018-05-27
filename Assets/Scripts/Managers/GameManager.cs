using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    private float playTime;

    void Awake()
    {
        instance = this;
    }

    public void EndGame() {
        //On game ending, saves the last score into player prefs and loads the end scene
        ScoreObject s = new ScoreObject(CoinManager.instance.GetCoins(), (int) playTime);
        SaveLoad.SaveLastScore(s.ToString());
        SceneManagement.LoadScene(2);
    }

    void Update()
    {
        //Updates health, coins and time indicators on the canvas and keeps a game timer
        playTime += Time.deltaTime;
        CanvasManager.instance.SetTime(playTime);
        CanvasManager.instance.SetHealth(HealthManager.instance.GetHealth());
        CanvasManager.instance.SetCoins(CoinManager.instance.GetCoins());
    }

    public float GetTime() {
        return playTime;
    }
}
