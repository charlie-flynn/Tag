using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _playerOne = true;

    [SerializeField]
    private float _acceleration = 1.0f;

    [SerializeField]
    private float _maxSpeed = 10.0f;

    [SerializeField]
    private float _jumpPower = 10.0f;

    [SerializeField]
    private float _dashPower = 15.0f;

    [SerializeField]
    private float _dashCooldownDuration = 2.0f;

    [SerializeField]
    private float _WavedashWindow = 0.1f;


    private Rigidbody _rigidbody;

    private float _moveInput;

    private bool _canJump;

    private float _dashCooldown;

    private float _wavedashCooldown;

    private bool _isDashing;

    public bool IsPlayerOne { get { return _playerOne; } }

    private void OnCollisionEnter(Collision collision)
    {
        _isDashing = false;
    }

    private void OnTriggerStay(Collider other)
    {
        _canJump = true;

        if (_isDashing)
        {
            _wavedashCooldown -= Time.deltaTime;

            if (_wavedashCooldown < 0.0f)
                _isDashing = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canJump = false;
        _wavedashCooldown = _WavedashWindow;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _canJump = false;
    }

    void Update()
    {
        _dashCooldown -= Time.deltaTime;

        // if player one, use player1horizontal, otherwise use player2horizontal
        if (_playerOne)
        {
            _moveInput = Input.GetAxisRaw("Player1Horizontal");

            if (Input.GetKeyDown(KeyCode.W))
                Jump();

            if (Input.GetKeyDown(KeyCode.LeftShift))
                Dash();
        }
        else
        {
            _moveInput = Input.GetAxisRaw("Player2Horizontal");

            if (Input.GetKeyDown(KeyCode.UpArrow))
                Jump();

            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.RightControl))
                Dash();
        }
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        if (_isDashing) return;

        Vector3 deltaMovement = new Vector3();
        deltaMovement.x = _moveInput * _acceleration;
        _rigidbody.AddForce(deltaMovement * Time.fixedDeltaTime, ForceMode.VelocityChange);

        Vector3 newVelocity = _rigidbody.velocity;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -_maxSpeed, _maxSpeed);
        _rigidbody.velocity = newVelocity;

        //_rigidbody.AddForceAtPosition(Vector3.left * _acceleration * Time.fixedDeltaTime, new Vector3(-0.3f, -1.0f), ForceMode.VelocityChange);

    }

    void Jump()
    {
        if (!_canJump) return;

        _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.VelocityChange);

        _canJump = false;
    }

    void Dash()
    {
        if (_dashCooldown > 0.0f || _moveInput == 0 || _canJump) return;

        _rigidbody.AddForce(new Vector3(_moveInput * _dashPower, 1.0f), ForceMode.Impulse);

        _dashCooldown = _dashCooldownDuration;
        _isDashing = true;
    }
}
