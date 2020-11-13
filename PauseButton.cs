using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Stage
    {
        Continue,
        Restart,
        StageSelect,
        ReturnTitle,
        Quit
    }

    public Stage Target;

    public Canvas PauseCanvas;

    private LevelLoader LL;

    void Start()
    {
        PauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        LL = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        switch (Target)
        {
            case Stage.Continue:

                PauseCanvas.enabled = false;
                Time.timeScale = 1f;
                break;
            case Stage.Restart:
                Time.timeScale = 1f;
                StartCoroutine(LL.LoadLevel(SceneManager.GetActiveScene().buildIndex));
                Time.timeScale = 1f;
                break;
            case Stage.StageSelect:
                Time.timeScale = 1f;
                StartCoroutine(LL.LoadLevel(1));
                Time.timeScale = 1f;
                break;
            case Stage.ReturnTitle:
                Time.timeScale = 1f;
                StartCoroutine(LL.LoadLevel(0));
                Time.timeScale = 1f;
                break;
            case Stage.Quit:
                Application.Quit();
                break;

        }
    }
}
