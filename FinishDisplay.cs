using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDisplay : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(KillSelf());
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
