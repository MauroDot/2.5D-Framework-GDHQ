using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameInput : MonoBehaviour
{
    private GameMaps _gameInput;
    public event EventHandler OnInteract;
    private void Awake()
    {
        _gameInput = new GameMaps();
    }

    private void OnEnable()
    {
        _gameInput.Player.Enable();
        _gameInput.Player.Interaction.performed += Interaction_performed;

    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public bool IsRolling()
    {
        return _gameInput.Player.Roll.IsInProgress();
    }

    public bool GrabInteraction()
    {
        return _gameInput.Player.Interaction.IsPressed();
    }

    public Vector3 GetLadderValueNormalized() 
    {
        Vector3 UpNDown = _gameInput.Player.Ladder.ReadValue<Vector2>();
        UpNDown = UpNDown.normalized;
        return UpNDown;
    }

    public Vector3 GetMovementVectorNoramlized() 
    {
       Vector3 _moveDirection = _gameInput.Player.Move.ReadValue<Vector2>();

        _moveDirection = _moveDirection.normalized;

        return _moveDirection;
    }

    public bool IsJumping()
    {
        if (_gameInput.Player.Jump.IsPressed())
            return true;
        else return false;
    }
}
