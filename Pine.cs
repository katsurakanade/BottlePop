using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pine : MonoBehaviour
{

    [Header("設定")]
    [Tooltip("回転速度")]
    [Range(0,500)]
    public float RotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime));
    }
}
