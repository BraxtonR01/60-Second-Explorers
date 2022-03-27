using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameObject player;
    private PlayerStats stats;
    private Text timeOutText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        timeOutText = stats.timeOverText;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.timeLeft > 0)
        {
            stats.timeLeft -= Time.deltaTime;
            stats.timeText.text = stats.ConvertTime();
        }
        else
        {
            stats.timeText.text = "00:00";

            StartCoroutine(WaitTime());
        }
    }

    IEnumerator WaitTime()
    {

        timeOutText.enabled = true;
        player.GetComponent<PlayerMovement>().paused = true;

        yield return new WaitForSeconds(2);


        timeOutText.enabled = false;
        player.GetComponent<PlayerMovement>().paused = false;

        player.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = 5;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().offset = 2;
        stats.timeLeft = stats.maxTime;
        stats.timeText.text = stats.ConvertTime();
        stats.health = stats.maxHealth;

        Destroy(this);
    }
}
