using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int direction = 1;
    public int damage;
    public int health;
    public bool updated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().HealthChange(-damage);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
            updated = false;
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {

            health -= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damage;
            if (health <= 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().CoinChange(1);
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            if (!collision.GetComponent<Bullet>().pierce)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}