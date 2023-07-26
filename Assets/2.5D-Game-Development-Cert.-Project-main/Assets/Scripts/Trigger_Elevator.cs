
using System;
using UnityEngine;

public class Trigger_Elevator : MonoBehaviour
{
    public event EventHandler OnCallElevator;
    [SerializeField] Transform _callButton;
    [SerializeField] Moving_Platform _platform;
    private MeshRenderer meshRendererCallButton;
    private GameInput _gameInput;
    private Player player;
    public void Start()
    {
        _platform.GetComponent<Moving_Platform>();
        meshRendererCallButton = _callButton.GetComponent<MeshRenderer>();
        if (_gameInput == null)
        {
            _gameInput = FindObjectOfType<Player>().GetComponent<GameInput>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Player>(out player);

        if (other.CompareTag("Player"))
        {
            _gameInput.OnInteract += ActivateElevator;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameInput.OnInteract -= ActivateElevator;
        }
    }


    private void ActivateElevator(object sender, System.EventArgs e)
    {
         meshRendererCallButton.material.color = Color.green;
        _platform.Moving_Platform_OnCallElevator();
    }
}
