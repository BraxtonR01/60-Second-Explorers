using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool playerNear;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerStats>().hasKey)
            {
                Destroy(gameObject.transform.parent.gameObject);
                Destroy(gameObject);
            }
        }
    }
    public void Update()
    {
        if(playerNear && (Input.GetKeyDown(KeyCode.E))){ 
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-50, -200, 0);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 8;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().offset = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
