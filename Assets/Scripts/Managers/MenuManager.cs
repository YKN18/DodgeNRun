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

        AsyncOperation asyncLoad = SceneManagement.LoadAsync(1);

        while (!asyncLoad.isDone)
        {
            bar.GetComponent<Bar>().currentPercent = asyncLoad.progress * 100;
            yield return null;
        }
    }

    public void Leaderboard()
    {
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
        SaveLoad.SaveEffectsVolume(effectsVolume.value);
        SaveLoad.SaveSoundVolume(soundVolume.value);
        settingsPanel.SetActive(false);
    }

    public void Tutorial() {
        panelT1.SetActive(true);
    }

    public void T1Btn() {
        panelT1.SetActive(false);
        panelT2.SetActive(true);
    }

    public void T2Btn() {
        panelT2.SetActive(false);
        panelT3.SetActive(true);
    }

    public void Home() {
        panelT1.SetActive(false);
        panelT2.SetActive(false);
        panelT3.SetActive(false);
    }

}
