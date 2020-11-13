using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerCord : MonoBehaviour
{

    public bool On;
    public float NeedSize;

    private GameObject l2d;

    // Start is called before the first frame update
    void Start()
    {
        On = false;
        l2d = transform.GetChild(3).transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (On)
        {
            l2d.SetActive(true);
        }

        else if (!On)
        {
            l2d.SetActive(false);
        }
    }


}
