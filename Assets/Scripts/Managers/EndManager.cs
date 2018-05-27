using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {
    [SerializeField] Text yourScore;
    [SerializeField] GameObject insertNamePanel, leaderboardPanel, creditsPanel;
    [SerializeField] Text playerName;
    [SerializeField] Text namesText, timesText, coinsText;
   
    private ScoreObject s;

    public void Menu() {
        SceneManagement.LoadScene(0);
    }

    public void Awake() {
        s = new ScoreObject(SaveLoad.LoadLastScore());
        yourScore.text = "You lasted " + s.GetTime() + " secs and collected " + s.GetCoins() + " coins";  
    }

    public void Leaderboard() {
        leaderboardPanel.SetActive(true);
        Leaderboard leaderboard;

        namesText.ResetText();
        timesText.ResetText();
        coinsText.ResetText();
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

    public void Close() {
        leaderboardPanel.SetActive(false);
    }
    public void Cancel() {
        insertNamePanel.SetActive(false);
    }

    public void Save()
    {
        Leaderboard leaderboard;
        string leaderboard_string = SaveLoad.LoadLeaderboard();
        if (leaderboard_string != "")
        {
            leaderboard = JsonUtility.FromJson<Leaderboard>(SaveLoad.LoadLeaderboard());
        }
        else
        {
            leaderboard = new Leaderboard();
        }
        leaderboard.Insert(new Entry(playerName.text, s, System.DateTime.Now));
        SaveLoad.SaveLeaderboard(JsonUtility.ToJson(leaderboard));
        insertNamePanel.SetActive(false);
    }

    public void Credits() {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits() {
        creditsPanel.SetActive(false);
    }
}
