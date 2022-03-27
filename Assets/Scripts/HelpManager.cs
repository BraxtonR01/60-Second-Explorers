using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public GameObject helpPanel;
    private bool menuOpen;
    public GameObject helpMenu;

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().timeLeft < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxTime)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (helpMenu.activeInHierarchy)
            {
                helpMenu.SetActive(false);
            }
            else
            {
                helpMenu.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Application.Quit();
            Debug.Log("quit");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            helpPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().paused = true;
            menuOpen = true;

        }

        if (menuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                helpPanel.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().paused = false;
                menuOpen = false;
            }
        }
    }
}
