using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class title : MonoBehaviour
{
    public AudioSource music;
    public AudioClip Click;
    public AudioClip Select;
    private float hInput, vInput;
    public enum Stage
    {
        StageSelect,
        SampleScene,
        Options,
        Exit,
    }

    public Stage Target;


    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        Click = Resources.Load<AudioClip>("music/SE/choiceSE");
        Select = Resources.Load<AudioClip>("music/SE/selectSE");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        if ((Input.anyKeyDown || hInput != 0 || vInput != 0) && !Input.GetButtonDown("Controller_A"))
        {
            music.clip = Select;
            music.Play();
        }
    }

    public void OnClick()
    {
        music.clip = Click;
        music.Play();

        switch (Target)
        {
            case Stage.StageSelect:
                //Time.timeScale = 1f;
                SceneManager.LoadScene("StageSelect");
                break;
            case Stage.SampleScene:
                //Time.timeScale = 1f;
                SceneManager.LoadScene("Stage1");
                break;
            case Stage.Options:
                break;
            case Stage.Exit:
                //Time.timeScale = 0f;
                Application.Quit();
                break;
        }
    }
}
