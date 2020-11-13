using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private AudioSource music;
    public AudioClip Click;

    public Canvas m_canvas;
    private GameObject LL;

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();


        LL = GameObject.Find("LevelLoader");
        LL.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_canvas.enabled) 
        {

            if (!Camera.main.GetComponent<AudioSource>().isPlaying)
            {
                Camera.main.GetComponent<AudioSource>().Play();
            }

            

            StartCoroutine(AudioController.FadeIn(Camera.main.GetComponent<AudioSource>(), 5.0f, 0.5f,0.01f));
            if (Input.GetButtonDown("Controller_A") || Input.GetKeyDown(KeyCode.Space))
            {
                music.PlayOneShot(Click);
                StartCoroutine(DelayActive());
            }
            
        }
    }

    IEnumerator DelayActive()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LL.GetComponent<LevelLoader>().LoadLevel(1));
    }
}
