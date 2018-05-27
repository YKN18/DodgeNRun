using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHealth : Powerup<int> {

    private int life = (int) PowerupType.bonusLife;
    //Extends the powerup class
    override public int GetBonus()
    {
        //Return the extra lifes from the powerup
        return life;
    }
}
