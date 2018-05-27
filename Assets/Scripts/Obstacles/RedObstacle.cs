using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedObstacle :  MonoBehaviour, Damaging{

    [SerializeField] int damage = 1;

    public int GetDamage()
    {
        //Returns the damage dealt by the red cube
        return damage;
    }
}
