using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineTrigger : MonoBehaviour
{

    public CinemachineVirtualCamera self;
    public CinemachineVirtualCamera target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            self.Priority = 1;
            target.Priority = 10;
        }
    }
}
