using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Entry {
    public string playerName;
    public ScoreObject score;
    public int timestamp;

    public Entry(string p, ScoreObject s, DateTime t) {
        playerName = p;
        score = s;
        DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        timestamp = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
    }
}
