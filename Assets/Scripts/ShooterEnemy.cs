using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int shootDmg;
    public int speed;

    public GameObject bullet;

    public Transform bulletSpawn;
    public float bulletForce = 20f;

    public float attackSpeed = .5f;
    private float coolDown;
    public int direction = 1;

    private void Start()
    {
        gameObject.GetComponentInChildren<EnemyDamage>().damage = damage;
        gameObject.GetComponentInChildren<EnemyDamage>().health = health;
    }

    void Update()
    {

        direction = gameObject.GetComponentInChildren<EnemyDamage>().direction;
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (direction == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Time.time > coolDown)
            {
                Shoot("right");
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (Time.time > coolDown)
            {
                Shoot("left");
            }
        }

    }
    void Shoot(string direction)
    {
        GameObject bull = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        bull.GetComponent<Bullet>().dmg = shootDmg;

        if (direction == "right")
        {
            rb.AddForce(bulletSpawn.right * bulletForce, ForceMode2D.Impulse);
            bull.transform.Rotate(0, 0, 90);
        }
        if (direction == "left")
        {
            rb.AddForce(bulletSpawn.right * -bulletForce, ForceMode2D.Impulse);
            bull.transform.Rotate(0, 0, -90);
        }

        coolDown = Time.time + attackSpeed;
    }
}
