using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scales : MonoBehaviour
{

    [Header("設定")]
    [Range(1,20)]
    [Tooltip("需要の水量")]
    public int NeedWater;
    private AudioSource _audio;
    public AudioClip Goal;
    bool OneShot;

    [Header("状態")]
   
    public bool Pass;

    //private TextMeshPro AmountText;
    private GameObject top;

    private float DownValue;

    // Start is called before the first frame update
    void Start()
    {
        OneShot = true;
        Pass = false;
        _audio = GetComponent<AudioSource>();
        top = transform.GetChild(0).gameObject;
        //AmountText = transform.GetChild(3).GetComponent<TextMeshPro>();
        //AmountText.text = NeedWater.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus p))
            {
                if (p.Water >= NeedWater)
                {
                    if(OneShot==true)
                    {

                        _audio.PlayOneShot(Goal);
                        OneShot = false;

                    }
                    Pass = true;
                }

                else
                {
                    p.dialog.Use = true;
                    p.dialog.String = "あと" + (NeedWater - p.Water).ToString();
                }
            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus p))
            {
               
              p.dialog.Use = false;
            }
        }
    }


}
