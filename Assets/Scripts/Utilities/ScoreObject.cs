[System.Serializable]
public class ScoreObject  {
    public int coins;
    public int time;

    public ScoreObject(int c, int t) {
        coins = c;
        time = t;
    }

    public ScoreObject(string s) {
        int[] values = System.Array.ConvertAll<string, int>(s.Split(','), int.Parse);
        coins = values[0];
        time = values[1];
    }

    override public string ToString() {
        return coins + "," + time;
    }
    public int GetCoins() {
        return coins;
    }

    public int GetTime()
    {
        return time;
    }
}
