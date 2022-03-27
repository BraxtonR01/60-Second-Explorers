using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStart : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.AddComponent<Timer>();
            collision.gameObject.GetComponent<PlayerStats>().hasKey = false;
            GameObject eh = GameObject.FindGameObjectWithTag("EnemyHolder");
            GameObject.FindGameObjectWithTag("Key").SetActive(true);
            foreach(Transform t in eh.transform)
            {
                t.gameObject.SetActive(true);
            }
        }
    }

}
