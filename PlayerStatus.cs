using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
	[Header("設定")]
	[Tooltip("スケール変更量")]
	public Vector3 ScaleChange;
	public int MaxScaleMultiplier = 5;

	[Tooltip("GUI")]
	public GameObject GUIController;
	public GameObject WaterCollider, BubbleCollider;

	[Header("状態")]
	[Tooltip("タイプ(水、バブル)")]
	public StatusMode InitialStatus;
	[Tooltip("持っている水")]
	public int Water;
	[Tooltip("状態制限されている")]
	public bool Limited;

	[Header("RayCast Settings")]
	public float GroundedRayCastDistance = 1.0f;

	[Header("RayCast Positions")] 
	public Vector3 WaterLeft;

	public GameObject[] waterRayCastPos;
	public GameObject[] bubbleRayCastPos;
	
	public LayerMask RayCastLayer;

	[HideInInspector]
	public DialogController dialog;

	public List<GameObject> groundedObjects;

	public bool canChangeState = true;

    public AudioClip WaterToBubble, BubbleToWater;

	private PlayerController _playerController;
	private Animator _animator;
    private AudioSource _audio;
    private ParticleSystem _particleSystem;
	private Vector3 _startScale;
	private int _oldWaterCount;
	private static readonly int Type = Animator.StringToHash("Type");
	private static readonly int ChangeToBubble = Animator.StringToHash("ChangeToBubble");
	private static readonly int ChangeToWater = Animator.StringToHash("ChangeToWater");

	private void Awake()
	{
		GameManager.instance.playerStatus = this;
        GameManager.instance.Player = this.gameObject;
	}

	private void Start()
	{
		groundedObjects = new List<GameObject>();
		_startScale = transform.localScale;

		AddStatus(InitialStatus);

		_playerController = GetComponent<PlayerController>();
		_animator = GetComponentInChildren<Animator>();
        _audio = GetComponent<AudioSource>();
        _particleSystem = GetComponent<ParticleSystem>();
		dialog = transform.GetComponentInChildren<DialogController>();

	}

	private void Update()
	{
		if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Controller_A")) && canChangeState && !Limited)
		{
			canChangeState = false;

			StateChange();
		}

		if(GetPlayerGroundedStatus()) canChangeState = true;

		if (GUIController.TryGetComponent(out GUIController gui)){
			gui.SetScore(Water);
			gui.SetPlayerStatus(this);
		}

		CalculatePlayerScale();
	}

	public void StateChange()
	{
		_particleSystem.Play();

		if (HasStatus(StatusMode.Water))
		{
            _audio.PlayOneShot(WaterToBubble);
            
			_animator.SetInteger(Type, 1);
			_animator.SetTrigger(ChangeToBubble);
			AddStatus(StatusMode.Bubble);
			RemoveStatus(StatusMode.Water);

			BubbleCollider.SetActive(true);
			WaterCollider.SetActive(false);
		}
		else if (HasStatus(StatusMode.Bubble))
		{
            _audio.PlayOneShot(BubbleToWater);
            _animator.SetInteger(Type, 0);
			_animator.SetTrigger(ChangeToWater);
			AddStatus(StatusMode.Water);
			RemoveStatus(StatusMode.Bubble);

			WaterCollider.SetActive(true);
			BubbleCollider.SetActive(false);
		}
	}

	private bool GetPlayerGroundedStatus()
	{
		if (HasStatus(StatusMode.Water))
		{
			groundedObjects.Clear();
			bool isGrounded = false;
			foreach (GameObject go in waterRayCastPos)
			{
				RaycastHit2D hit = Physics2D.Raycast(go.transform.position, Vector2.down,
					GroundedRayCastDistance * transform.localScale.y, RayCastLayer); 
				if (hit)
				{
					groundedObjects.Add(hit.collider.gameObject);
					isGrounded = true;
				}
			}
			return isGrounded;
		}
		if (HasStatus(StatusMode.Bubble))
		{
			groundedObjects.Clear();
			bool isGrounded = false;
			foreach (GameObject go in bubbleRayCastPos)
			{
				RaycastHit2D hit = Physics2D.Raycast(go.transform.position, Vector2.up,
					GroundedRayCastDistance * transform.localScale.y, RayCastLayer);
				if (hit)
				{
					groundedObjects.Add(hit.collider.gameObject);
					isGrounded = true;
				}
			}
			return isGrounded;
		}
		return false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Vector2 distance = new Vector2(0, GroundedRayCastDistance * transform.localScale.y);
		
		if (HasStatus(StatusMode.Water))
		{
			foreach (GameObject go in waterRayCastPos)
			{
				Vector2 pos = go.transform.position;
				Gizmos.DrawLine(pos, pos - distance);
			}
		}
		
		if (HasStatus(StatusMode.Bubble))
		{
			foreach (GameObject go in bubbleRayCastPos)
			{
				Vector2 pos = go.transform.position;
				Gizmos.DrawLine(pos, pos + distance);
			}
		}
	}

	private void CalculatePlayerScale()
	{
		int waterCount = Water <= MaxScaleMultiplier ? Water : MaxScaleMultiplier;

		if (_oldWaterCount == waterCount) return;
		transform.localScale = _startScale + (ScaleChange * waterCount);
		_oldWaterCount = waterCount;
	}
}
