using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingEnemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int speed;
    // Update is called once per frame
    void Update()
    {
        MoveEnemy(gameObject.GetComponentInChildren<EnemyDamage>().direction);
    }

    //-1 is left, 1 is right
    public void MoveEnemy(int direction) {
        if(direction == 1)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

    }
}
