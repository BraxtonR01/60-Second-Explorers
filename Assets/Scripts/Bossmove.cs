using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bossmove : MonoBehaviour
{
    public int health;
    public int damage;
    public int speed;
    public int direction;

    public GameObject bullet;

    public List<Transform> bulletSpawns;
    public float bulletForce = 20f;

    public float attackSpeed = .5f;
    private float coolDown;

    public TextMesh healthText;

    // Update is called once per frame
    void Update()
    {
        MoveEnemy(direction);
        healthText.text = health.ToString();
    }


    public void MoveEnemy(int direction)
    {
        if (direction == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Time.time > coolDown)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().HealthChange(-damage);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damage;
            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    void Shoot()
    {
        foreach(Transform bulletSpawn in bulletSpawns)
        {
            GameObject bull = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
            bull.GetComponent<Bullet>().dmg = damage;
            rb.AddForce(bulletSpawn.up * -bulletForce, ForceMode2D.Impulse);
            bull.transform.Rotate(0, 0, 180);
            }
        coolDown = Time.time + attackSpeed;

    }
}