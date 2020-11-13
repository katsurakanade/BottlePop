using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    
    public GameObject waterDrop;
    private AudioSource _audio;
    public AudioClip GetCandy,
        GrapeJump,CollisionThorn;
    [Range(1,30)]
    public float FlashSpeed = 5.0f;
    public float FlashDuration = 1.0f;
    public float PushBackForceAgainstEnemy = 1000.0f;
    public float WaterDropSpeedSquared = 160000;
    public float inputPauseTime = 1.0f;
    //public float WaterDropSpeedMaxSquared = 360000;
    //public float MinNumberOfWaterDrop = 1;
    //public float MaxNumberOfWaterDrop = 4;

    private PlayerStatus _playerStatus;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private static readonly int WaterJump = Animator.StringToHash("WaterJump");
    private static readonly int BubbleJump = Animator.StringToHash("BubbleJump");

    private void Awake()
    {
        GameManager.instance.playerCollision = this;
    }

    private void Start()
    {
        //UI.SetActive(false);
        _onTriggerHashSet = new HashSet<GameObject>();

        _playerStatus = GetComponent<PlayerStatus>();

        _audio = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _onTriggerHashSet.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Land"))
        {
            float sqrRelativeVelocity = collision.relativeVelocity.sqrMagnitude;

            //Calculate ballistics. Simulate g force applied depending on contact angle
            float angle = Vector2.Angle(collision.relativeVelocity, collision.contacts[0].normal);
            angle = Mathf.InverseLerp(1, 0, angle / 90);

            if (sqrRelativeVelocity * angle > WaterDropSpeedSquared && _playerStatus.Water > 0)
            {

                //int dropWater = Mathf.FloorToInt(Mathf.Lerp(MinNumberOfWaterDrop, MaxNumberOfWaterDrop, sqrRelativeVelocity / WaterDropSpeedMaxSquared));
                //if (dropWater > _playerStatus.Water) dropWater = _playerStatus.Water;

                //int dropWater = _playerStatus.Water;
                //for (int i = 0; i < dropWater; i++)
                //{
                //    GameObject waterGo = Instantiate(waterDrop, transform.position, Quaternion.identity);
                //    waterGo.GetComponent<WaterFromPlayer>().Target = -collision.contacts[0].normal;
                //}
                //_playerStatus.Water -= dropWater;
                
                //if (_playerStatus.HasStatus(Status.StatusMode.Water))
                //{
                //    _animator.SetTrigger(WaterJump);
                //    _playerController.PauseUserInput(inputPauseTime);
                //}else if (_playerStatus.HasStatus(Status.StatusMode.Bubble))
                //{
                //    _animator.SetTrigger(BubbleJump);
                //    _playerController.PauseUserInput(inputPauseTime);
                //}
            }
        }

        if (collision.collider.CompareTag("BubblePlate"))
        {
            if (!_playerStatus.HasStatus(Status.StatusMode.Water)) return;
            _playerStatus.StateChange();
            _playerStatus.canChangeState = false;
            return;
        }

        if (collision.collider.CompareTag("Thorn"))
        {
            if (!_playerStatus.HasStatus(Status.StatusMode.Bubble)) return;
            _playerStatus.StateChange();
            _playerStatus.canChangeState = false;

            _audio.PlayOneShot(CollisionThorn);
            
            _rb.AddRelativeForce(PushBackForceAgainstEnemy * collision.contacts[0].normal, ForceMode2D.Impulse);
        }

        if (collision.collider.CompareTag("Clear"))
        {
           
            if (GameManager.instance.barController.clear)
            {

                GameManager.instance.GameIsClear = true;
                GameManager.instance.hud.enabled = false;

                if (GameObject.Find("Turorial Canvas") != null)
                {
                    GameObject.Find("Turorial Canvas").GetComponent<Canvas>().enabled = false;
                }

                GameObject.Find("LevelTop").GetComponent<BottlePop>().PopCap();
                GameManager.instance.soundManger.PlaySE(GameManager.instance.soundManger.PopLevelPop);

                //GameObject FireWorks = Resources.Load("Effect/FireWorks") as GameObject;
                //Instantiate(FireWorks, GameManager.instance.Player.transform.position + new Vector3(100, -30, 0), GameManager.instance.Player.transform.rotation);
                //Instantiate(FireWorks, GameManager.instance.Player.transform.position + new Vector3(-100, -30, 0), GameManager.instance.Player.transform.rotation);

                StartCoroutine(AudioController.FadeOut(Camera.main.GetComponent<AudioSource>(), 3.0f));

                GameManager.instance.sceneClearTime = Time.time;
                StartCoroutine(GameManager.instance.clearmovie.DelayPlayVideo(1.5f));
            }

           
        }

    }

    private HashSet<GameObject> _onTriggerHashSet;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_onTriggerHashSet.Contains(other.gameObject))
        {
            if (other.CompareTag("Water"))
            {
                _audio.PlayOneShot(GetCandy);

                GameManager.instance.GetComponent<BarController>().AddValue();
                _playerStatus.Water++;
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Jelly"))
            {
                if (!_playerStatus.HasStatus(Status.StatusMode.Damaged))
                {
                    StartCoroutine(RegisterDamage((transform.position - other.transform.position).normalized));
                }
            }

            

            _onTriggerHashSet.Add(other.gameObject);
        }
    }
    
  
    private IEnumerator RegisterDamage (Vector2 direction)
    {
        _playerStatus.AddStatus(Status.StatusMode.Damaged);
        if (_playerStatus.HasStatus(Status.StatusMode.Water))
        {
            _animator.SetTrigger(WaterJump);
        }else if (_playerStatus.HasStatus(Status.StatusMode.Bubble))
        {
            _animator.SetTrigger(BubbleJump);
        }
        _playerStatus.Water--;
        if(_playerStatus.Water < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _rb.AddForce(direction * PushBackForceAgainstEnemy, ForceMode2D.Impulse);

        float timer = FlashDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            float level = Mathf.Abs(Mathf.Sin(Time.time * FlashSpeed));
            _spriteRenderer.color = new Color(1f, 1f, 1f, level);

            yield return null;
        }
        _playerStatus.RemoveStatus(Status.StatusMode.Damaged);
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    

}