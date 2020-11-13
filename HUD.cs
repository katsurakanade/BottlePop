using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private Text RightStickText;

    // Start is called before the first frame update
    void Start()
    {
        RightStickText = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        GameManager.instance.hud = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedRightStickText(string str)
    {
        RightStickText.text = str;
    }
}
