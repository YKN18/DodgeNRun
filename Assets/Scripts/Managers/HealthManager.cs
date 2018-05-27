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
        //Upon hit of a damaging GameObject, reduces the health by the 'damage' amount
        //If the health is below 0, the game ends. While the player is invincible, it
        //can't be hit by damaging GameObjects.
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
        //Increase the health by 'inc' as long as the resulting healt is below a threshold
        if(health + inc <= maxHealth)
            health += inc;
    }

    public void SetInvincible(bool b) {
        //Prevents the health to decrement as long as the invincible powerup is active
        isInvincible = b;
    }

}
