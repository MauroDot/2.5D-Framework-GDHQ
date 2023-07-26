using UnityEngine;

public class ClimbUpBehavior : StateMachineBehaviour
{
    [SerializeField] private Vector3 _standingPosition;
    private Player _player;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = FindObjectOfType<Player>();
        if (_player != null)
        {
           _player.StandingUp();
        }
    }
}
