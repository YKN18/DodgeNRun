using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoad {

    //Collection of methods to save and load data from player prefs

    public static void SaveLastScore(string lastScore) {
        PlayerPrefs.SetString("LASTSCORE", lastScore);
    }

    public static string LoadLastScore() {
        if (PlayerPrefs.HasKey("LASTSCORE"))
            return PlayerPrefs.GetString("LASTSCORE");
        return "";
    }

    public static void SaveLeaderboard(string leaderboard)
    {
        PlayerPrefs.SetString("LEADERBOARD", leaderboard);
    }

    public static string LoadLeaderboard() {
        if (PlayerPrefs.HasKey("LEADERBOARD"))
            return PlayerPrefs.GetString("LEADERBOARD");
        return "";
    }

    public static void DeletePrefs() {
        PlayerPrefs.DeleteAll();
    }

    public static float LoadSoundVolume() {
        if (PlayerPrefs.HasKey("SOUNDVOLUME"))
            return PlayerPrefs.GetFloat("SOUNDVOLUME");
        return 0.6f;
    }


    public static void SaveSoundVolume(float v) {
        PlayerPrefs.SetFloat("SOUNDVOLUME", v);
    }


    public static float LoadEffectsVolume() {
        if (PlayerPrefs.HasKey("EFFECTSVOLUME"))
            return PlayerPrefs.GetFloat("EFFECTSVOLUME");
        return 0.25f;
    }


    public static void SaveEffectsVolume(float v) {
        PlayerPrefs.SetFloat("EFFECTSVOLUME", v);
    }
}


