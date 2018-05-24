using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] Text namesText, coinsText, timesText;
    [SerializeField] GameObject bar, progressPanel, leaderboardPanel, settingsPanel;
    [SerializeField] Slider soundVolume, effectsVolume;
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
}
