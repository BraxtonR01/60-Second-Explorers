using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;

    private int jumpCount;
    public int maxJump;
    public int jumpPower;

    public bool paused = false;
    public GameObject bullet;

    public Transform bulletSpawn;
    public float bulletForce = 20f;

    public float attackSpeed = .5f;
    private float coolDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        jumpPower *= 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && (jumpCount < maxJump))
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;
        }

        if (Input.GetKey(KeyCode.UpArrow) && Time.time>coolDown)
        {
            Shoot("up");
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Time.time > coolDown)
        {
            Shoot("right");
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Time.time > coolDown)
        {
            Shoot("down");
        }
        else if (Input.GetKey(KeyCode.LeftArrow)&& Time.time > coolDown)
        {
            Shoot("left");
        }
    }
    void Shoot(string direction)
    {
        GameObject bull = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();

        if (direction == "up")
        {
            rb.AddForce(bulletSpawn.up * bulletForce, ForceMode2D.Impulse);
        }
        if (direction == "right")
        {
            rb.AddForce(bulletSpawn.right * bulletForce, ForceMode2D.Impulse);
            bull.transform.Rotate(0, 0, 90);
        }
        if (direction == "down")
        {
            rb.AddForce(bulletSpawn.up * -bulletForce, ForceMode2D.Impulse);
            bull.transform.Rotate(0, 0, 180);
        }
        if (direction == "left")
        {
            rb.AddForce(bulletSpawn.right * -bulletForce, ForceMode2D.Impulse);
            bull.transform.Rotate(0, 0, -90);
        }

        coolDown = Time.time + attackSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
