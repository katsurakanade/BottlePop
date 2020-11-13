using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Banana : MonoBehaviour
{
    [Range(0, 100f)]
    public float RotateSpeed;
    float rotationAngle;
    Transform father;

    // Start is called before the first frame update
    void Start()
    {
        father = this.transform;
        rotationAngle = RotateSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        var child = father.GetChild(0).gameObject;
        
        transform.RotateAround(child.transform.position, new Vector3(0, 0, -1), rotationAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != null)
        {
            rotationAngle = 0f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name != null)
        {
            rotationAngle = RotateSpeed * Time.deltaTime; ;
        }
    }
}
