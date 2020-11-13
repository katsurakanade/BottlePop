using System.Collections;
using UnityEngine;

public class WaterFromPlayer : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("移動速度")]
    [Range(50.0f, 1000.0f)]
    public float moveSpeed;
    
    public float convertToCollectibleTime;
    public float stopMotionTime;
    [Header("Radians +-")]
    [Range(0, 2f)] 
    public float randomSpread;

    public Vector2 Target
    {
        get => _target;
        set => _target = value;
    }

    private Rigidbody2D _rb;
    private Vector2 _target;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
            
        StartCoroutine(DelayTriggerEnabled());
        
        //Get random point on unit circle
        var randomRadian = Mathf.PI * randomSpread;
        var randomAngle = Random.Range(-randomRadian, randomRadian);
        var ca = Mathf.Cos(randomAngle);
        var sa = Mathf.Sin(randomAngle);
        
        Vector2 randomVector = new Vector2(_target.x * ca - _target.y * sa, _target.x * sa + _target.y * ca).normalized;

        _rb.AddRelativeForce(randomVector * moveSpeed, ForceMode2D.Impulse);        
    }

    private IEnumerator DelayTriggerEnabled()
    {
        yield return new WaitForSeconds(convertToCollectibleTime);
        gameObject.layer = LayerMask.NameToLayer("Collectible");
        
        yield return new WaitForSeconds(stopMotionTime);
        
        if (TryGetComponent(out CircleCollider2D circleCollider))
        {
            circleCollider.isTrigger = true;
        }
        Destroy(_rb);
    }
}
