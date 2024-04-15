using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float initialMoveSpeed = 5f;
    
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f; // Duration of the dash in seconds
    public float dashCooldown = 1f; // Cooldown time for dashing
    
    private float _nextDashTime = 0f;
    private float _moveSpeed;

    private Rigidbody _rigidbody;
    private GameController _gameController;
    
    private Vector3 _movementInput;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        _moveSpeed = initialMoveSpeed;
    }
    
    void Update()
    {
        // Don't allow actions if the game is paused
        if (_gameController.IsPaused())
        {
            return;
        }

        // Get input from the player
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Create a movement vector
        _movementInput = new Vector3(moveX, 0f, moveZ).normalized;

        // Check if player is pressing any movement keys
        if (_movementInput != Vector3.zero)
        {
            // Apply movement to the rigidbody
            _rigidbody.velocity = _movementInput * _moveSpeed;
        }
        else
        {
            // If no movement keys are pressed, stop the player
            _rigidbody.velocity = Vector3.zero;
        }

        // Check for dash input and cooldown
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextDashTime)
        {
            StartCoroutine(Dash());
            _nextDashTime = Time.time + dashCooldown; // Set next dash time after cooldown
        }
    }

    IEnumerator Dash()
    {
        _moveSpeed = dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        
        _moveSpeed = initialMoveSpeed;
    }
}
