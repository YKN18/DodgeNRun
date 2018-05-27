using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, Takeable<int> {

    [SerializeField] int coinValue = 5;

    public int GetBonus() {
        //Returns the coin worth
        return coinValue;
    }
}
