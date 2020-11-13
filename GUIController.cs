using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public GameObject Water, Bubble, Amedama, AmedamaCounter, AmedamaCounter2;
    public float AmedamaRotationRate = 20.0f;
    private RawImage _counter, _counter2;
    void Start()
    {
        _counter = AmedamaCounter.GetComponent<RawImage>();
        _counter2 = AmedamaCounter2.GetComponent<RawImage>();
        SetScore(0);
    }

    void Update()
    {
        Amedama.transform.Rotate(Vector3.forward * AmedamaRotationRate * Time.deltaTime);
    }

    public void SetScore(int score)
    {
        int firstDigit = score % 10;
        int secondDigit = (score % 100) / 10;

        float x = (float)firstDigit / 10;
        _counter.uvRect = new Rect(x, 0, 0.1f, 1);
        x = (float)secondDigit / 10;
        _counter2.uvRect = new Rect(x, 0, 0.1f, 1);
    }

    public void SetPlayerStatus(Status status)
    {
        if (status.HasStatus(Status.StatusMode.Water))
        {
            Water.SetActive(true);
            Bubble.SetActive(false);
        }
        else
        if(status.HasStatus(Status.StatusMode.Bubble))
        {
            Water.SetActive(false);
            Bubble.SetActive(true);
        }
    }
}