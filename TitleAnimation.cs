using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{

    public AudioClip op_audio;
    public Canvas main_c;
    private GameObject op;


    private AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        op = GameObject.Find("op_0");
        StartCoroutine(ChangeBGColor());
        StartCoroutine(ChangeOpLayer());

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(op_audio);
        StartCoroutine(AudioFadeOutStart());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeOpLayer()
    {
        yield return new WaitForSeconds(2.7f);
        op.GetComponent<SpriteRenderer>().sortingOrder = -2;
    }
    
    IEnumerator ChangeBGColor()
    {
        yield return new WaitForSeconds(3);
        Camera.main.backgroundColor = new Color(0.54f,0.87f,0.91f,0);
        main_c.enabled = true;
       
    }

    IEnumerator AudioFadeOutStart()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(AudioController.FadeOut(audioSource, 3.0f));
    }
}
