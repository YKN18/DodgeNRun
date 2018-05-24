using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvincible : Powerup<float>{
    private float invincibleTime = (float)PowerupType.invincibleTime;

    override public float GetBonus()
    {
        return invincibleTime;
    }
}
