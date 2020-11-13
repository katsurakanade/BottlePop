using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jelly : MonoBehaviour
{

    public bool Can_Move;

    // 移動速度
    public float MoveSpeed = 1.0f;

    // 点
    public GameObject[] Node;

    public int Arrow = 0;

    public bool is_dmg;

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
        if (Can_Move && Node[0] != null)
        {
            // 行く場所決める
            for (int i = 0; i < Node.Length - 1; i++)
            {
                if (transform.position == Node[i].transform.position)
                {
                    transform.Rotate(0, 180, 0);
                    Arrow++;
                }
            }

            if (transform.position == Node[Node.Length - 1].transform.position)
            {
                transform.Rotate(0, 180, 0);
                Arrow = 0;
            }


            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (is_dmg)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //{
        //    if (collision.transform.parent.TryGetComponent<PlayerStatus>(out PlayerStatus ps))
        //    {
        //        if (ps.Water <= 0)
        //        {
        //            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //        }

        //        else if (ps.Water > 0)
        //        {
        //            ps.Water--;
        //        }
        //    }
        //}
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
