using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvincible : Powerup<float>{
    private float invincibleTime = (float)PowerupType.invincibleTime;

    override public float GetBonus()
    {
        //Returns the time during which the player is invincible
        return invincibleTime;
    }
}
