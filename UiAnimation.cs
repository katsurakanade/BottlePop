using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiAnimation : MonoBehaviour
{
    public GameObject UI_1, UI_2;


    [Range(0f,5f)]
    public float time_1 = 1f;
    [Range(0f, 5f)]
    public float time_2 = 0.01f;
    public Canvas main_c;

    public float perRad_1 = 0.03f;
    public float perRad_2 = 0.02f;
    float rad_1 = 0f,rad_2 = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (rad_1 >= 8f || rad_1 <= -10f) {
            perRad_1 = -perRad_1;
        }
        rad_1 += perRad_1;
        if (rad_2 >= 7f || rad_2 <= -7f)
        {
            perRad_2 = -perRad_2;
        }
        rad_2 += perRad_2;
        if (main_c.enabled)
        {
            UI_1.transform.Rotate(0, 0, perRad_1);
            //UI_2.transform.Rotate(0, 0, perRad_2);
            if (UI_1.transform.localPosition.y <= 270f)
            {
                UI_1.transform.Translate(Vector3.Normalize(new Vector3(0f, 270f, 0f) - new Vector3(0f, -640f, 0f)) * (Vector3.Distance(new Vector3(0f, -640f, 0f), 
                    new Vector3(0f, 270f, 0f)) / (time_1 / Time.deltaTime)));
                
            }
            else if (UI_1.transform.localPosition.y >= 270f)
            {
                if (UI_2.transform.localPosition.y <= -300f)
                {
                    UI_2.transform.Translate(Vector3.Normalize(new Vector3(0f, -300f, 0f) - new Vector3(0f, -640f, 0f)) * (Vector3.Distance(new Vector3(0f, -640f, 0f),
                        new Vector3(0f, -300f, 0f)) / (time_2 / Time.deltaTime)));
                }
            }
            
        }
        
    }


}
