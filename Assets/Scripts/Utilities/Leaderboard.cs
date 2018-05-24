using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leaderboard {
    public List<Entry> leaderboard;
    private int capacity;

    public Leaderboard() {
        leaderboard = new List<Entry>();
        capacity = 10;
    }

    public void Insert(Entry entry)
    {
        leaderboard.Add(entry);

        leaderboard.Sort((first, second) => {
            var time = second.score.time.CompareTo(first.score.time);
            if (time == 0)
            {
                var coins = second.score.coins.CompareTo(first.score.coins);
                if (coins == 0) return first.timestamp.CompareTo(second.timestamp);
                else return coins;
            }
            else return time;
        });

        while (leaderboard.Count > capacity) {
            leaderboard.RemoveAt(capacity);
        }
    }
}
