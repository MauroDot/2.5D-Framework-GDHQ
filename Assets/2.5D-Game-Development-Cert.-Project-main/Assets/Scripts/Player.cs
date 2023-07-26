using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameInput _gameInput;
    private CharacterController _controller;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpStrength = 15.0f;
    [SerializeField] private float _gravity;
    [SerializeField] private bool _groundPlayer;

    [SerializeField] private float _coinsCollected;
   
    private Vector3 _standUp;
    
    private float _yVelocity;
    public float XVelocity { get; private set; }

    private bool _isFlipedAxis;
    public bool isRunning { get; private set;}
    public bool isJumping { get; private set; }
    public bool isHanging { get; set; }
    public bool isClimbing { get; set; }

    private void Awake()
    {
         _gameInput = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
        if (_gameInput == null) Debug.LogError("Missing Game Input");
        if (_controller == null) Debug.LogError("Missing Character Controller");
    }


    private void Update()
    {
        
        if(!isHanging)
        Movement();
    }


    private void Movement() 
    {

        _groundPlayer = _controller.isGrounded;
        isJumping = _gameInput.IsJumping();

        if (_groundPlayer == true && !isClimbing)
        {
            _controller.Move(Vector3.zero);
            _yVelocity = -_gravity;
        }

        if (_groundPlayer == true && _gameInput.IsJumping())
        {
            _yVelocity += _jumpStrength;
        }

        if (_groundPlayer == false && !isClimbing)
            _yVelocity -= _gravity * Time.deltaTime;

        var _yMaxVelocity = Mathf.Clamp(_yVelocity, -20, 100f);

        Vector3 _direction = _gameInput.GetMovementVectorNoramlized();
        Vector3 _xVelocity = _direction * _speed;
        
        if(_direction.x == -1 && !_isFlipedAxis) 
        {
            _isFlipedAxis = true;
            transform.Rotate(Vector3.up, 180);
        }
        else if (_direction.x == 1 && _isFlipedAxis) 
        {
            _isFlipedAxis = false;
            transform.Rotate(Vector3.up, 180);
        }


        if (isClimbing == true)
        {
            _yVelocity = 0;
            Vector3 ladderDirection = _gameInput.GetLadderValueNormalized();
            _yVelocity += ladderDirection.y * 2f;

        }


        Vector3 _movement = new Vector3(0, _yMaxVelocity, _xVelocity.x);
        XVelocity = _direction.x;
        isRunning = _direction != Vector3.zero;
        _controller.Move(_movement * Time.deltaTime);

    }

    public void UpdateCoinsCollected()
    {
        _coinsCollected++;
    }

    public void GrabLedge(Vector3 snapPosition)
    {
        transform.position = snapPosition;
        isHanging = true;
        XVelocity = 0;
        isJumping = false;
        _controller.enabled = false;
    }

    public void SetStandingUpPosition(Vector3 snapPosition) => _standUp = snapPosition;

    public void StandingUp() { 
    
        transform.position = _standUp;
        isHanging = false;
        _controller.enabled = true;
    }
}
