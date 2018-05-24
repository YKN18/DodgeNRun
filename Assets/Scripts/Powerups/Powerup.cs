using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup<T> : MonoBehaviour,  Takeable<T> {
    public enum PowerupType { bonusLife = 1, slowTime = 5, invincibleTime = 5 };
    public PowerupType MyPowerup;
    protected T bonus;

    virtual public T GetBonus() {
        return bonus;
    }
    
}
