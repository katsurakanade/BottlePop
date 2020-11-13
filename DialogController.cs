using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{

    public string String;
    public bool Use;

    private Animator animator;

    private TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        Use = false;
        animator = GetComponent<Animator>();
        tmp = transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Use)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            animator.SetBool("Play", true);
            StartCoroutine(ShowText(0.5f));
        }

        else if (!Use)
        {
            animator.SetBool("Play", false);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            tmp.enabled = false;
        }
    }

    IEnumerator ShowText(float time)
    {
        yield return new WaitForSeconds(time);

        if (Use)
        {
            tmp.text = String;
            tmp.enabled = true;
        }
        
    }

   
    
}
