using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public static HealthManager instance;
    [SerializeField] int health = 3;
    [SerializeField] GameObject player;
    private int maxHealth = 5;
    private bool isInvincible = false;
    private float lastHit = 0f;

    void Awake()
    {
        instance = this;
    }

    public void Hit(int damage) {
        if (isInvincible || GameManager.instance.GetTime() - lastHit < 1) return;
        health -= damage;
        SoundManager.instance.PlayHit();
        player.GetComponent<Animator>().Play("playerHit");
        lastHit = GameManager.instance.GetTime();
        if (health <= 0) {
            GameManager.instance.EndGame();
        }
        
    }

    public int GetHealth() {
        return health;
    }

    public void IncreaseHealth(int inc) {
        if(health + inc <= maxHealth)
            health += inc;
    }

    public void SetInvincible(bool b) {
        isInvincible = b;
    }

}
