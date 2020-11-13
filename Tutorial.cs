using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tutorial : MonoBehaviour
{

    private GameObject Frame;
    private GameObject Text;
    

    // Start is called before the first frame update
    void Start()
    {
        Frame = transform.GetChild(0).gameObject;
        Text = Frame.transform.GetChild(0).gameObject;

        Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.barController.collectedCount >= GameManager.instance.barController.clearCount)
        {
            Frame.GetComponent<Animator>().SetBool("Play", true);
            StartCoroutine(DisplayText());
            StartCoroutine(CloseDisplay());
        }

        
    }

    IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(1.0f);
        Text.SetActive(true);
    }

    IEnumerator CloseDisplay()
    {
        yield return new WaitForSeconds(5.0f);
        Text.SetActive(false);
        Frame.SetActive(false);
    }
}
