using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BarController : MonoBehaviour
{
    public GameObject gauge;

    public int clearCount;
    public int collectedCount;
    [Range(1, 99)]
    public int maxCount = 5;
    
    [Header("Animation Settings")]
    public float expansionSize = 1.3f;
    public int oscillationCount = 3;
    public float oscillationTime = 0.03f;

    public float indicatorSpeed = 1.0f;

    public bool clear;
    
    private RectTransform _bodyTransform, _centerTransform, _outsideTransform;
    private Quaternion _targetAngle = Quaternion.identity;

    private bool EffectPlayed;

    private void Awake()
    {
        GameManager.instance.barController = this;
    }

    public void AddValue()
    {
        collectedCount++;
        _targetAngle *= _stepValue;
        Animate();
    }

    private Quaternion _stepValue;
    private void Start()
    {
        _stepValue = Quaternion.Euler(new Vector3(0,0,-360.0f / maxCount));
            
        var center = gauge.transform.GetChild(1).gameObject;
        var outside = gauge.transform.GetChild(0).gameObject;

        _centerTransform = center.GetComponent<RectTransform>();
        _centerTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        
        _outsideTransform = outside.GetComponent<RectTransform>();
        _outsideTransform.rotation = Quaternion.Euler(new Vector3(0, 0, clearCount * -360.0f / maxCount));

        _bodyTransform = gauge.GetComponent<RectTransform>();
        _startScale = _bodyTransform.localScale;
        _targetScale = _startScale * expansionSize;

        EffectPlayed = false;
    }
    
    private void Update()
    {
        //Go crazy if collected max
        if (collectedCount >= maxCount)
        {
            _centerTransform.Rotate(Vector3.forward, -20);
                
            return;
        }
        
        if (_centerTransform.rotation != _targetAngle)
        {
            _centerTransform.rotation = Quaternion.Slerp(_centerTransform.rotation, _targetAngle, Time.deltaTime * indicatorSpeed);
        }

        if (collectedCount >= clearCount && Camera.main.GetComponent<AudioSource>().pitch < 1.5f)
        {
            //Camera.main.GetComponent<AudioSource>().pitch = 1.5f;

            if (!EffectPlayed)
            {
                GameManager.instance.soundManger.PlaySE(GameManager.instance.soundManger.CanClear);
                GameObject FireWorks = Resources.Load("Effect/FinishDisplay") as GameObject;
                Instantiate(FireWorks, GameManager.instance.Player.transform.position + new Vector3(-30, 30, 0), Quaternion.Euler(0,0,90));

                //Instantiate(FireWorks, GameManager.instance.Player.transform.position + new Vector3(-150, -30, 0), GameManager.instance.Player.transform.rotation);
                EffectPlayed = true;
            }

            clear = true;
        }
    }
    
    private bool _animationRunning;
    
    private void Animate()
    {
        if (!_animationRunning)
        {
            StartCoroutine(Animation());
        }
    }

    private Vector3 _startScale;
    private Vector3 _targetScale;
    private IEnumerator Animation()
    {
        Vector3 velocity = Vector3.zero;
        bool flag = true;
        int count = oscillationCount;
        _animationRunning = true;
        while (count > 0)
        {
            if (flag)
            {
                _bodyTransform.localScale = Vector3.SmoothDamp(_bodyTransform.localScale, _targetScale, ref velocity, oscillationTime);

                if ((_bodyTransform.localScale - _targetScale).sqrMagnitude < .001f) flag = false;
            }
            else
            {
                _bodyTransform.localScale = Vector3.SmoothDamp(_bodyTransform.localScale, _startScale, ref velocity, oscillationTime);

                if ((_bodyTransform.localScale - _startScale).sqrMagnitude < .001f)
                {
                    count--;
                    flag = true;
                }
            }
            yield return null;
        }
        _animationRunning = false;
    }

  
}
