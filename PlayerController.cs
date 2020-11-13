using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("移動力")]
    public float MovementForce;
    public ForceMode2D MovementForceMode;
    public float altMovementForce = 5000;
    
    [Tooltip("水平制限速度")]
    [Range(0, 1000)]
    public float HorizontalSpeedLimit = 10.0f;
    
    [Range(0, 5000)]
    public float VerticalSpeedLimit = 10.0f;

    // [Tooltip("ジャンプ速度")]
    // [Range(0, 5000)]
    // public float JumpForce;
    //
    // [Range(0, 5.0f)]
    // public float JumpInputDelay = 0;
    //
    // [Range(0, 5.0f)]
    // public float TimeBeforeNextJump = 0.2f;
    //
    // [Range(0, 1.0f)]
    // public float JumpRetardationRate = 0.9f;

    [Tooltip("下重力")]
    [Range(10, 100)]
    public float GravityDown = 10.0f;

    [Tooltip("上重力")]
    [Range(10, 100)]
    public float GravityUp = 5.0f;
    public float InputDeadzone = 0.1f;

    public bool canMove = true;
    public bool speedLimiter = true;

    //public AudioClip transWater,
    //    transBuble;


    private Rigidbody2D _rb;
    private AudioSource _audio;
    private AudioSource _loopAudio;

    private Animator _animator;
    private PlayerStatus _playerStatus;
    [HideInInspector]
    public float horizontalInput;
    // private bool _canJump = true;
    // private bool _retardJump;
    private bool _jumpSubroutine;


    private void Awake()
    {
        GameManager.instance.playerController = this;
    }

    private void Start()
    {
        if (TryGetComponent(out Rigidbody2D rb))
        {
            _rb = rb;
        }

        if (TryGetComponent(out PlayerStatus s))
        {
            _playerStatus = s;
        }
        _loopAudio = GetComponent<AudioSource>();
        _audio = GetComponent<AudioSource>();
        _animator = GetComponentInChildren<Animator>();
        _loopAudio.loop = true;
        _loopAudio.playOnAwake = false;

    }

    private float _jumpTimer;
    private static readonly Quaternion Left = Quaternion.Euler(0, -180, 0);
    private static readonly int IsWalkingWater = Animator.StringToHash("is_walking_water");

    private void Update()
    {
        //End of jump detection
        // if (!_jumpSubroutine)
        // {
        //     _jumpTimer += Time.deltaTime;
        //     if (_jumpTimer > TimeBeforeNextJump)
        //     {
        //         if (_playerStatus.GetPlayerGroundedStatus())
        //         {
        //             _canJump = true;
        //             _jumpTimer = 0;
        //             _retardJump = false;
        //         }
        //         else
        //         {
        //             _canJump = false;
        //         }
        //     }
        // }

        // Jumping Function
        //if (canMove && (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Controller_A")))
        //{
        //    if (_canJump)
        //    {
        //        audio.PlayOneShot(JumpSound);

        //        _canJump = false;
        //        _jumpTimer = 0;
        //        _retardJump = true;
        //        if (_playerStatus.HasStatus(Status.StatusMode.Water))
        //        {

        //            StartCoroutine(Jump(JumpForce));
        //        }
        //        else if (_playerStatus.HasStatus(Status.StatusMode.Bubble))
        //        {

        //            StartCoroutine(Jump(-JumpForce));
        //        }
        //    }
        //}
        ////Jump Retardation
        ////If space is not pressed slow down jump speed
        //else if(!_jumpSubroutine && _retardJump)
        //{
        //    Vector2 velocity = _rb.velocity;
        //    if (_playerStatus.HasStatus(Status.StatusMode.Water))
        //    {
        //        if (velocity.y < -2f) _retardJump = false;

        //        if (velocity.y > 0)
        //        {
              
        //            _rb.velocity = new Vector2(velocity.x, velocity.y * JumpRetardationRate);
        //        }
        //    }
        //    else if (_playerStatus.HasStatus(Status.StatusMode.Bubble))
        //    {
        //        if (velocity.y > 2f) _retardJump = false;

        //        if (velocity.y < 0)
        //        {
              

        //            _rb.velocity = new Vector2(velocity.x, velocity.y * JumpRetardationRate);
        //        }
        //    }
        //}
        
        //Stop animation if there is no input
        if (Mathf.Abs(horizontalInput) < InputDeadzone)
        {
            _animator.SetBool(IsWalkingWater, false);
        }
        
        //Set gravity direction
        StatusProcess();
    }

    private void FixedUpdate()
    {
        //Main Controls
        horizontalInput = Input.GetAxis("Horizontal");
        //_verticalInput = Input.GetAxis("Vertical");
        
        Vector2 velocity = _rb.velocity;
        
        if (canMove && horizontalInput > InputDeadzone)
        {
            transform.localRotation = Quaternion.identity;
            if (_playerStatus.groundedObjects.Any())
            {
                //if (!_loopAudio.isPlaying && _playerStatus.HasStatus(Status.StatusMode.Water))
                //{
                //    _loopAudio.PlayOneShot(transWater);
                //}
                //if (!_loopAudio.isPlaying && _playerStatus.HasStatus(Status.StatusMode.Bubble))
                //{
                //    _loopAudio.PlayOneShot(transBuble);
                //}
                _rb.AddForce(new Vector2(MovementForce * Time.deltaTime, 0), MovementForceMode);
            }
            else
            {
                _rb.AddForce(new Vector2(altMovementForce * Time.deltaTime, 0), MovementForceMode);
            }
            
            _animator.SetBool(IsWalkingWater, true);
        }

        if (canMove && horizontalInput < -InputDeadzone)
        {
            //if (_audio.isPlaying) _audio.PlayOneShot(transWater);


            transform.localRotation = Left;
            if (_playerStatus.groundedObjects.Any())
            {
                //if (!_loopAudio.isPlaying&&_playerStatus.HasStatus(Status.StatusMode.Water))
                //{
                //    _loopAudio.PlayOneShot(transWater);
                //}
                //if (!_loopAudio.isPlaying && _playerStatus.HasStatus(Status.StatusMode.Bubble))
                //{
                //    _loopAudio.PlayOneShot(transBuble);
                //}
                _rb.AddForce(new Vector2(-MovementForce * Time.deltaTime, 0), MovementForceMode);
            }
            else
            {
                _rb.AddForce(new Vector2(-altMovementForce * Time.deltaTime, 0), MovementForceMode);
            }
            
            _animator.SetBool(IsWalkingWater, true);
        }
        
        //Speed Limiter
        if (speedLimiter)
        {
            if (Mathf.Abs(velocity.x) > HorizontalSpeedLimit)
            {
                Vector2 normalized = velocity.normalized;

                _rb.velocity = Mathf.Abs(velocity.y) > VerticalSpeedLimit ? 
                    new Vector2(normalized.x * HorizontalSpeedLimit, normalized.y * VerticalSpeedLimit) : 
                    new Vector2(normalized.x * HorizontalSpeedLimit, velocity.y);
            }else if (Mathf.Abs(velocity.y) > VerticalSpeedLimit)
            {
                Vector2 normalized = velocity.normalized;

                _rb.velocity = Mathf.Abs(velocity.x) > HorizontalSpeedLimit ? 
                    new Vector2(normalized.x * HorizontalSpeedLimit, normalized.y * VerticalSpeedLimit) : 
                    new Vector2(velocity.x, normalized.y * VerticalSpeedLimit);
            }
        }
    }

    //Set gravity for whether bubble or water
    private void StatusProcess()
    {
        if (_playerStatus.HasStatus(Status.StatusMode.Water))
        {
            _rb.gravityScale = GravityDown;
        }
        else if (_playerStatus.HasStatus(Status.StatusMode.Bubble))
        {
            _rb.gravityScale = -GravityUp;
        }
    }
    
    // private IEnumerator Jump(float force)
    // {
    //     _jumpSubroutine = true;
    //
    //     yield return new WaitForSeconds(JumpInputDelay);
    //
    //     _rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    //     _jumpSubroutine = false;
    // }

    public void PauseUserInput(float time)
    {
        StartCoroutine(_pauseInput(time));
    }
    
    private IEnumerator _pauseInput(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}