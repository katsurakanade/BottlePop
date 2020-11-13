using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [Header("設定")]
    [Tooltip("偏移")]
    public Vector3 OffSet;
    [Tooltip("速度")]
    public float Speed;
    [Tooltip("条件オブジェクト")]
    public List<GameObject> TargetList;

    [Header("状態")]
    [Tooltip("開き")]
    public bool Open;

    private Vector3 TopStartPos;
    private Vector3 DownStartPos;
    private Vector3 TopFinishPos;
    private Vector3 DownFinishPos;

    private int PassCount;

    // Start is called before the first frame update
    void Start()
    {
        TopStartPos = transform.GetChild(0).position;
        DownStartPos = transform.GetChild(1).position;
        TopFinishPos = transform.GetChild(0).position + OffSet;
        DownFinishPos = transform.GetChild(1).position - OffSet;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject obj in TargetList)
        {
            if (obj.TryGetComponent<Scales>(out Scales s))
            {
                if (s.Pass)
                {
                    PassCount++;
                }
            }

            else if (obj.TryGetComponent<PowerCord>(out PowerCord pc))
            {
                if (pc.On)
                {
                    Open = true;
                }

                else if (!pc.On)
                {
                    Open = false;
                }
            }
        }

        if (PassCount >= TargetList.Capacity)
        {
            Open = true;
        }

        PassCount = 0;


        if (Open)
        {
            transform.GetChild(0).transform.position = Vector3.MoveTowards(transform.GetChild(0).transform.position, TopFinishPos, Speed * Time.deltaTime);
            transform.GetChild(1).transform.position = Vector3.MoveTowards(transform.GetChild(1).transform.position, DownFinishPos, Speed * Time.deltaTime);
        }

        else if (!Open)
        {
            transform.GetChild(0).transform.position = Vector3.MoveTowards(transform.GetChild(0).transform.position, TopStartPos, Speed * Time.deltaTime);
            transform.GetChild(1).transform.position = Vector3.MoveTowards(transform.GetChild(1).transform.position, DownStartPos, Speed * Time.deltaTime);
        }
    }
}
