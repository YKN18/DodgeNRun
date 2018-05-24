using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedObstacle :  MonoBehaviour, Damaging{

    [SerializeField] int damage = 1;

    public int GetDamage()
    {
        return damage;
    }
}
