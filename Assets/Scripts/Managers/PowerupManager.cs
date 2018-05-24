using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] GameObject slowBar, invincibleBar;
    public static PowerupManager instance;
    private float ts, ti, slowTime, invincibleTime;
    private float curSpeed;
    private bool isSlow, isInvincible;

    void Awake()
    {
        instance = this;
    }

    public void TriggerHealthPowerup(GameObject powerup)
    {
        HealthManager.instance.IncreaseHealth(powerup.GetComponent<PowerupHealth>().GetBonus());
    }

    public void TriggerSlowPowerup(GameObject powerup)
    {
        if (isSlow)
        {
            slowTime += powerup.GetComponent<PowerupSlow>().GetBonus();
        }
        else
        {
            slowTime = powerup.GetComponent<PowerupSlow>().GetBonus();
            curSpeed = player.GetComponent<Player>().GetSpeed();
            player.GetComponent<Player>().SetSpeed(curSpeed / 2);
            isSlow = true;
            StartCoroutine("ResetSpeed");
        }
    }

    public void TriggerInvinciblePowerup(GameObject powerup)
    {
        if (isInvincible)
        {
            invincibleTime += powerup.GetComponent<PowerupInvincible>().GetBonus();
        }
        else
        {
            invincibleTime = powerup.GetComponent<PowerupInvincible>().GetBonus();
            HealthManager.instance.SetInvincible(true);
            isInvincible = true;
            StartCoroutine("ResetInvincible");
        }
        

    }

    IEnumerator ResetSpeed() {
        ts = 0;
        slowBar.GetComponent<Bar>().SetPercent((int)((1 - ts / slowTime) * 100));
        slowBar.transform.parent.gameObject.SetActive(true);
        while (ts < slowTime)
        {
            ts += Time.deltaTime;
            slowBar.GetComponent<Bar>().SetPercent((int)((1 - ts / slowTime) * 100));
            yield return 0;
        }
        player.GetComponent<Player>().SetSpeed(curSpeed);
        slowBar.transform.parent.gameObject.SetActive(false);
        isSlow = false;
    }

    IEnumerator ResetInvincible()
    {
        ti = 0;
        invincibleBar.GetComponent<Bar>().SetPercent((int)((1 - ti / invincibleTime) * 100));
        invincibleBar.transform.parent.gameObject.SetActive(true);
        while (ti < invincibleTime)
        {
            ti += Time.deltaTime;
            invincibleBar.GetComponent<Bar>().SetPercent((int)((1-ti/invincibleTime)*100));
            yield return 0;
        }
        HealthManager.instance.SetInvincible(false);
        invincibleBar.transform.parent.gameObject.SetActive(false);
        isInvincible = false;
    }

    public void CurrentSpeedIncreased(float speedup) {
        curSpeed += speedup;
    }
}
