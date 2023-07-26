using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [SerializeField] Player _player;
    private GameInput _gameInput;
    private const string isJumping = "Jumping";
    private const string isRolling = "Roll";
    private const string isLadder = "Ladder";
    private const string isClimbing = "ClimbUp";
    private const string isHanging = "LedgeGrab";

    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _gameInput = GetComponentInParent<GameInput>();
        if (_gameInput == null) Debug.LogError("Missing Game Input");
        _gameInput.OnInteract += _gameInput_OnInteract;
    }

    private void RollAnimation() { 
        if(_gameInput.IsRolling() && !_player.isJumping && !_player.isHanging)
        {
            _playerAnimator.SetTrigger(isRolling);
        }
        else
            _playerAnimator.ResetTrigger(isRolling);
    }

    private void _gameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (_player.isHanging) 
        { 
            _playerAnimator.SetTrigger(isClimbing);
        }
    }

    private void Update()
    {
        _playerAnimator.SetBool(isJumping, _player.isJumping);
        _playerAnimator.SetFloat("Speed", Mathf.Abs(_player.XVelocity));
        _playerAnimator.SetBool(isHanging, _player.isHanging);
        _playerAnimator.SetBool(isLadder, _player.isClimbing);
        RollAnimation();
    }
    
}
