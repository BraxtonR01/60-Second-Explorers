using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public int maxHealth;
    public int health;
    public int coins;
    public float timeLeft;
    public float maxTime;
    public int damage;

    public Text timeText;
    public Text coinText;
    public Text healthText;
    public Text timeOverText;
    public Text deathText;

    public bool hasKey;

    public void Start()
    {
        timeLeft = maxTime;
        health = maxHealth;
        coinText.text = coins.ToString();
        healthText.text = health.ToString();
        timeText.text = ConvertTime();
    }

    public string ConvertTime()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        if (seconds < 10)
        {
            return "0" + minutes + ":0" + seconds;
        }
        return "0" + minutes + ":" + seconds;

    }

    public void HealthChange(int change)
    {
        if (gameObject.GetComponent<PlayerMovement>().paused)
        {
            return;
        }
        health = health + change;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            StartCoroutine(WaitTime());
        }
    }
    IEnumerator WaitTime()
    {
        deathText.enabled = true;
        Destroy(gameObject.GetComponent<Timer>());
        gameObject.GetComponent<PlayerMovement>().paused = true;

        yield return new WaitForSeconds(2);

        gameObject.GetComponent<PlayerMovement>().paused = false;

        deathText.enabled = false;

        gameObject.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 5;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().offset = 2;
        timeLeft = maxTime;
        health = maxHealth;
        healthText.text = health.ToString();
        timeText.text = ConvertTime();

    }
    public void CoinChange(int change)
    {
        coins = coins + change;
        coinText.text = coins.ToString();
    }
}
