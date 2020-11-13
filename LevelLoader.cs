using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [Header("シーン入れ替えアニメーション")]
    public Animator transition;
    public float transitionTime = 1f;

    public AudioClip SE;
    private AudioSource audioSource;

    private Canvas loadingcanvas;
    private Slider loadingbar;
    private Text loadingtext;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        loadingcanvas = transform.GetChild(1).gameObject.GetComponent<Canvas>();

        loadingbar = loadingcanvas.transform.GetChild(0).GetComponent<Slider>();
        loadingtext = loadingcanvas.transform.GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadLevel(int index)
    {
        Debug.Log("Load");
        audioSource.PlayOneShot(SE);

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        StartCoroutine(LoadAsync(index));

    }

    IEnumerator LoadAsync(int s_index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(s_index);

        loadingcanvas.enabled = true;

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingbar.value = progress;

            loadingtext.text = progress * 100f + " %";

            yield return null;
        }
    }
}
