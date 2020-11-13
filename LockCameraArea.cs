using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockCameraArea : MonoBehaviour
{

    public CinemachineVirtualCamera vc;

    private float Cur_Player_x;

    private GameObject Player;
    private Camera mc;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        mc = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(vc.Follow == null)
        {
            if( (Player.transform.position.x >= mc.transform.position.x && Cur_Player_x <= mc.transform.position.x)
                || (Cur_Player_x >= mc.transform.position.x && Player.transform.position.x <= mc.transform.position.x)
                )
            {
                vc.Follow = Player.transform;
            }
        }

        Cur_Player_x = Player.transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            vc.Follow = null;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        vc.Follow = GameObject.Find("Player").transform;
    //    }
    //}
}
