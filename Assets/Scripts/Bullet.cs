using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public bool pierce = false;
    public int dmg;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Lifespan");
        rb.AddForce(rb.transform.up * speed * 10);
    }

    IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && gameObject.layer.Equals(6))
        {
            collision.gameObject.GetComponent<PlayerStats>().HealthChange(-dmg);
        }

        Destroy(gameObject);
    }
}