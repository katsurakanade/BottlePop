using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    // Start is called before the first frame update
    private float alphaSpeed = 0.5f;

    private CanvasGroup cg;

    void Start()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        cg.alpha -= alphaSpeed * Time.deltaTime;
        if(cg.alpha >= 1f || cg.alpha <= 0f)
        {
            alphaSpeed = -alphaSpeed;
        }
    }
}
