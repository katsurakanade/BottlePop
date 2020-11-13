using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelect : MonoBehaviour
{

    public List<GameObject> SceneIcon;

    public EventSystem es;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFirstSelect(int index)
    {
        es.enabled = false;
        es.firstSelectedGameObject = SceneIcon[index];
        es.enabled = true;
    }
}
