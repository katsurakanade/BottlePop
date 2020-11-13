using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundGrape : MonoBehaviour
{
    Rigidbody2D rb2;
    public float BoundSpeed;
    bool BoundCheck=false;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb2.velocity.y < 0.1f && rb2.velocity.y > -0.1f && BoundCheck ==true)
        {
            transform.Translate(new Vector2(0, BoundSpeed*100*Time.deltaTime));

        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Grape"))
        {
            BoundCheck = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        BoundCheck = false;

    }

}
