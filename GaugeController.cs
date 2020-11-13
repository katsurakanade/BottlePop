using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeController : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.GetComponent<BarController>().gauge = gameObject;
    }
}