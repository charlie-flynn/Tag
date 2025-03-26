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

    public bool IsPlayerOne { get { return _playerOne; } }

    private Rigidbody _rigidbody;

    private float _moveInput;

    private bool _canJump;

    [SerializeField]
    private float _jumpCooldownDuration = 0.10f;
    private float _jumpCooldown;

    private void OnTriggerEnter(Collider other)
    {
        if (_jumpCooldown > 0) return;

        _canJump = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _canJump = false;
    }

    void Update()
    {
        _jumpCooldown -= Time.deltaTime;

        // if player one, use player1horizontal, otherwise use player2horizontal
        if (_playerOne)
        {
            _moveInput = Input.GetAxisRaw("Player1Horizontal");

            if (Input.GetKeyDown(KeyCode.W))
                Jump();
        }
        else
        {
            _moveInput = Input.GetAxisRaw("Player2Horizontal");

            if (Input.GetKeyDown(KeyCode.UpArrow))
                Jump();
        }
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
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

        _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        _jumpCooldown = _jumpCooldownDuration;

        _canJump = false;
    }
}
