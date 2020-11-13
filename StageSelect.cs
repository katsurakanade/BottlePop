using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StageSelect : MonoBehaviour
{
    private AudioSource music;
    public AudioClip Click;
    public AudioClip Select;
    private float hInput, vInput;

    private LevelLoader LL;

    private EventSystem es;
    
    public int target;

    private void Awake()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Start is called before the first frame update
    void Start()
    {
        LL = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        if ((Input.anyKey || hInput != 0 || vInput != 0) && !Input.GetButton("Controller_A"))
        {
            
            music.clip = Select;
            music.Play();
        }
    }

    public void OnClick()
    {
        es.enabled = false;
        music.clip = Click;
        music.Play();

        StartCoroutine(LL.LoadLevel(target));
    }
}