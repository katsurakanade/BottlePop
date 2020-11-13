using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // 移動速度
    public float MoveSpeed = 1.0f;

    // 点
    public GameObject[] Node;

    public int Arrow = 0;


    // Start is called before the first frame update
    void Start()
    {
        Arrow = 0;

        if (Node[0] != null)
        {
            transform.position = Node[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Node[0] != null)
        {
            // 行く場所決める
            for (int i = 0; i < Node.Length - 1; i++)
            {
                if (transform.position == Node[i].transform.position)
                {
                    //transform.Rotate(0, 180, 0);
                    Arrow++;
                }
            }

            if (transform.position == Node[Node.Length - 1].transform.position)
            {
                //transform.Rotate(0, 180, 0);
                Arrow = 0;
            }


            Patrol();
        }
    }

    private void FixedUpdate()
    {

    }

    private void Patrol()
    {

        for (int i = 0; i < Node.Length; i++)
        {
            if (Arrow == i)
            {
                transform.position = Vector3.MoveTowards(transform.position, Node[i].transform.position, MoveSpeed * Time.deltaTime);
            }
        }
    }
}
