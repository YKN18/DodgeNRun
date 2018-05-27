using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] Text namesText, coinsText, timesText;
    [SerializeField] GameObject bar, progressPanel, leaderboardPanel, settingsPanel;
    [SerializeField] Slider soundVolume, effectsVolume;
    [SerializeField] GameObject panelT1, panelT2, panelT3;
    public void PlayButton()
    {
        progressPanel.SetActive(true);
        bar.GetComponent<Bar>().currentPercent = 0;
        StartCoroutine("LoadAsync");
    }

    IEnumerator LoadAsync()
    {
        //Loads the game scene asynchronously and updates the loading bar
        AsyncOperation asyncLoad = SceneManagement.LoadAsync(1);

        while (!asyncLoad.isDone)
        {
            bar.GetComponent<Bar>().currentPercent = asyncLoad.progress * 100;
            yield return null;
        }
    }

    public void Leaderboard()
    {
        //Loads the leaderboard from player prefs and prints it
        leaderboardPanel.SetActive(true);
        namesText.ResetText();
        timesText.ResetText();
        coinsText.ResetText();
        Leaderboard leaderboard;
        string leaderboard_string = SaveLoad.LoadLeaderboard();
        if (leaderboard_string != "")
        {
            leaderboard = JsonUtility.FromJson<Leaderboard>(leaderboard_string);
            foreach (Entry e in leaderboard.leaderboard)
            {
                namesText.text += e.playerName + "\n\n";
                timesText.text += e.score.GetTime().ToString() + "\n\n";
                coinsText.text += e.score.GetCoins().ToString() + "\n\n";
            }
        }
    }

    public void Settings()
    {
        effectsVolume.value = SaveLoad.LoadEffectsVolume();
        soundVolume.value = SaveLoad.LoadSoundVolume();
        settingsPanel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }


    public void SaveSettings() {
        //Updates sound and effects volume in playerprefs
        SaveLoad.SaveEffectsVolume(effectsVolume.value);
        SaveLoad.SaveSoundVolume(soundVolume.value);
        settingsPanel.SetActive(false);
    }

    public void Tutorial() {
        //Activates the tutorial panel
        panelT1.SetActive(true);
    }

    public void T1Btn() {
        //Activates the tutorial's second frame
        panelT1.SetActive(false);
        panelT2.SetActive(true);
    }

    public void T2Btn() {
        //Activates the tutorial's third frame
        panelT2.SetActive(false);
        panelT3.SetActive(true);
    }

    public void Home() {
        panelT1.SetActive(false);
        panelT2.SetActive(false);
        panelT3.SetActive(false);
    }

}
