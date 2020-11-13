using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    public GameObject firstselect;

    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Controller_Menu"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0f;

                UI.SetActive(true);

                if (Mathf.Approximately(Time.timeScale, 0f))
                {
                    return;
                }
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1f;
                UI.SetActive(false);

            }
        }
        if (Input.GetButtonDown("Controller_B"))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1f;
                UI.SetActive(false);

            }
        }
    }
}
