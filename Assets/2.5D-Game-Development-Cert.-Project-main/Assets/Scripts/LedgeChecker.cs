using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    private Player _player;
    private GameInput _gameInput;
    [SerializeField] float _snapPositionX,_snapPositionY,_snapPositionZ;
    private Vector3 _snapPosition;
    [SerializeField] private Vector3 _standPosition;

    private void Start()
    {
        _snapPosition = new Vector3(_snapPositionX, _snapPositionY, _snapPositionZ);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("LedgeChecker")) 
        {
            _player = FindObjectOfType<Player>();
            if(_player != null) 
            {
                _gameInput = _player.GetComponent<GameInput>();
                if (_gameInput.GrabInteraction())
                {
                    _player.GrabLedge(_snapPosition);
                    _player.SetStandingUpPosition(_standPosition);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("LedgeChecker"))
        {
            _player = FindObjectOfType<Player>();
            if (_player != null)
            {
                _player.isHanging = false;
            }
        }
    }
}
