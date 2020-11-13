using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColor : MonoBehaviour
{
    
    Button[] buttons;
    Text[] texts;

    // Start is called before the first frame update
    void Start()
    { 
        buttons = GetComponentsInChildren<Button>();
        texts = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if(EventSystem.current.currentSelectedGameObject == buttons[i].gameObject)
            {
                texts[i].color  = new Color(0f, 255f, 0f);
            }
            else texts[i].color = new Color(0f, 0f, 0f);
        }
        
    }
}
