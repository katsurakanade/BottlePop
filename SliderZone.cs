using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderZone : MonoBehaviour
{
    public float duration;
    private PlayerController _player;
    private Rigidbody2D _playerRb;

    private void Start()
    {
        _player = GameManager.instance.playerController;
        _playerRb = _player.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!_slideActive)
        {
            StartCoroutine(StopSpeedLimiter());
        }
        else
        {
            _timer = duration;
        }
    }

    private bool _slideActive;
    private float _timer;
    private IEnumerator StopSpeedLimiter()
    {
        _timer = duration;
        _player.speedLimiter = false;
        _slideActive = true;
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            yield return null;
        }
        _player.speedLimiter = true;
        _slideActive = false;
    }
}
