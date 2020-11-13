using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float force;
    private Rigidbody2D _playerRb;
    private void Start()
    {
        _playerRb = GameManager.instance.playerCollision.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _playerRb.AddForce(transform.right * force, ForceMode2D.Impulse);
    }
}
