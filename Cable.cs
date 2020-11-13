using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{

    private float NeedSize;

    // Start is called before the first frame update
    void Start()
    {
        NeedSize = transform.parent.GetComponent<PowerCord>().NeedSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            if (other.GetComponentInParent<PlayerStatus>().Water >= NeedSize && other.GetComponentInParent<PlayerStatus>().InitialStatus == Status.StatusMode.Water)
            {
                transform.parent.GetComponent<PowerCord>().On = true;
            }
        }

        if (other.tag == "Water")
        {
            transform.parent.GetComponent<PowerCord>().On = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
           transform.parent.GetComponent<PowerCord>().On = false;
   
        }

        if (collision.tag == "Water")
        {
            transform.parent.GetComponent<PowerCord>().On = false ;
        }
    }
}
