using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{

    public AudioClip CanClear;
    public AudioClip PopLevelPop;
    public AudioClip StageClear;
    public AudioClip Fireworks;

    private AudioSource AS;

    private void Awake()
    {
        GameManager.instance.soundManger = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySE(AudioClip ac)
    {
        AS.PlayOneShot(ac);
    }
}
