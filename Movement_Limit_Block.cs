using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Limit_Block : MonoBehaviour
{

    public enum Type
    {
        StoE,EtoS
    }

    [Header("設定")]
    [Tooltip("タイプ")]
    public Type type;
    [Tooltip("必須スケール(飴玉の数で取ってる)")]
    public int NeedScale;

    private GameObject Start_Door;
    private GameObject End_Door;

    private GameObject Target;
    private GameObject Player;

    private bool Locked;

    // Start is called before the first frame update
    void Start()
    {
        Start_Door = transform.GetChild(0).gameObject;
        End_Door = transform.GetChild(1).gameObject;
        
        if (type == Type.StoE)
        {
            Start_Door.SetActive(true);
            Target = Start_Door;
            Destroy(End_Door);
        }

        else if (type == Type.EtoS)
        {
            End_Door.SetActive(true);
            Target = End_Door;
            Destroy(Start_Door);
        }

        Player = GameObject.FindGameObjectWithTag("Player");
        Locked = false;
    }

    // Update is called once per frame
    void Update()
    {

        // This Code is Not Good
        if (Player.GetComponent<PlayerStatus>().Water < NeedScale && !Locked) 
        {
            Target.GetComponent<BoxCollider2D>().isTrigger = false;
            Target.GetComponent<SpriteRenderer>().color = Color.red;
        }

        else if (Player.GetComponent<PlayerStatus>().Water >= NeedScale && !Locked)
        {
            Target.GetComponent<BoxCollider2D>().isTrigger = true;
            Target.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponentInParent<PlayerStatus>().Water >= NeedScale)
            {
                Locked = true;
                Target.GetComponent<BoxCollider2D>().isTrigger = false;
                Target.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

    }

   




}
