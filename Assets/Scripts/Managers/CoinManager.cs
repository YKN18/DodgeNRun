using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

    private int coins; // da trasformare in private
    public static CoinManager instance;

    void Awake()
    {
        instance = this;
    }

    public void AddCoin(int coinValue) {
        coins += coinValue;
    }

    public int GetCoins() {
        return coins;
    }
}
