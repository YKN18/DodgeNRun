using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHealth : Powerup<int> {

    private int life = (int) PowerupType.bonusLife;

    override public int GetBonus()
    {
        return life;
    }
}
