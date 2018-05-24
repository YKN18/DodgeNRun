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
        ScoreObject s = new ScoreObject(CoinManager.instance.GetCoins(), (int) playTime);
        SaveLoad.SaveLastScore(s.ToString());
        SceneManagement.LoadScene(2);
    }

    void Update()
    {
        playTime += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.P)) CanvasManager.instance.Pause();
        CanvasManager.instance.SetTime(playTime);
        CanvasManager.instance.SetHealth(HealthManager.instance.GetHealth());
        CanvasManager.instance.SetCoins(CoinManager.instance.GetCoins());
    }

    public float GetTime() {
        return playTime;
    }
}
