using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BottlePop : MonoBehaviour
{
    public Rigidbody2D cap;
    public GameObject plate;
    public Vector2 force;
    public float torque = -2000f;

    private bool _popped;
    public void PopCap()
    {
        if (_popped) return;
        Destroy(plate);
        cap.bodyType = RigidbodyType2D.Dynamic;
        cap.gravityScale = 50;
        cap.AddForce(force, ForceMode2D.Impulse);
        cap.AddTorque(torque, ForceMode2D.Impulse);
        _popped = true;
        
    }
}