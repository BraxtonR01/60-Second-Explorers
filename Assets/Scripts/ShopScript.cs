using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public List<int> prices;
    private int index = 0;
    private int maxIndex;
    private GameObject child;
    private GameObject priceText;
    private GameObject coin;
    private bool shopOpen;
    public List<int> upgrades;
    private bool playerPresent = false;
    private GameObject player;
    public GameObject bullet;

    public enum shopType
    {
        health,
        move,
        jump,
        special,
        gun,
        time
    };
    public shopType type;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shopOpen = true;
        maxIndex = prices.Count;
        child = gameObject.transform.GetChild(0).gameObject;
        priceText = child.transform.GetChild(1).gameObject;
        coin = child.transform.GetChild(0).gameObject;
        priceText.GetComponent<TextMesh>().text = prices[index].ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerPresent = true;
            priceText.GetComponent<MeshRenderer>().enabled = true;
            coin.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerPresent = false;
            priceText.GetComponent<MeshRenderer>().enabled = false;
            coin.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    private void Update()
    {
        if (playerPresent)
        {
            PlayerStats stats = player.gameObject.GetComponent<PlayerStats>();
            if (Input.GetKeyDown(KeyCode.B) && stats.coins >= prices[index] && shopOpen)
            {
                PlayerMovement movement = player.gameObject.GetComponent<PlayerMovement>();
                stats.CoinChange(-prices[index]);

                switch (type)
                {
                    case shopType.health:
                        stats.maxHealth += upgrades[index];
                        stats.health = stats.maxHealth;
                        stats.HealthChange(0);
                        break;
                    case shopType.move:
                        movement.speed += upgrades[index];
                        break;
                    case shopType.jump:
                        movement.jumpPower += upgrades[index];
                        break;
                    case shopType.gun:
                        stats.damage += upgrades[index];
                        break;
                    case shopType.special:
                        if (index == 0)
                        {
                            movement.attackSpeed = movement.attackSpeed/2;
                            gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "Special\n(Double Jump)";
                        }
                        else if (index == 1)
                        {
                            movement.maxJump++;
                            gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "Special\n(Bullets Pierce)";
                        }
                        else if (index == 2)
                        {
                            bullet.GetComponent<Bullet>().pierce = true;
                        }
                        break;
                    case shopType.time:
                        stats.maxTime += upgrades[index];
                        stats.timeLeft = stats.maxTime;
                        stats.timeText.text = stats.ConvertTime();
                        break;
                }

                index++;
                if (index == maxIndex)
                {
                    shopOpen = false;
                }
                else
                {
                    priceText.GetComponent<TextMesh>().text = prices[index].ToString();
                }
                if (!shopOpen)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
