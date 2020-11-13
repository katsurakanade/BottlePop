using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ClearMovie : MonoBehaviour
{

    private VideoPlayer v_player;
    private GameObject GUI;
    private Canvas ClearCanvas;

    private void Awake()
    {
        GameManager.instance.clearmovie = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        v_player = GetComponent<VideoPlayer>();
        GUI = GameObject.Find("GUI").transform.GetChild(0).gameObject;
        ClearCanvas = GameObject.Find("GUI").transform.GetChild(1).gameObject.GetComponent<Canvas>();
    }

    public IEnumerator DelayPlayVideo(float time)
    {
        yield return new WaitForSeconds(time);
        GUI.SetActive(false);
        v_player.Play();
        StartCoroutine(DisplayScore());
        StartCoroutine(PlayClearSE());
        StartCoroutine(Stop());
    }

    IEnumerator DisplayScore()
    {
        yield return new WaitForSeconds(7.0f);
        GameManager.instance.scoring.StartScore();
        ClearCanvas.enabled = true;
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(10.0f);

        GameManager.instance.GameClear();
    }

    IEnumerator PlayClearSE()
    {
        yield return new WaitForSeconds(1.0f);
        GameManager.instance.soundManger.PlaySE(GameManager.instance.soundManger.StageClear);
    }
}
