using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSlow : Powerup<float> {
    private float slowTime = (float) PowerupType.slowTime;

    override public float GetBonus()
    {
        //Returns the time during which the player is slowed down
        return slowTime;
    }
}
