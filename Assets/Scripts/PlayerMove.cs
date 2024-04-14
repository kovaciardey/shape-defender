using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody _rigidbody;
    private GameController _gameController;
    
    private Vector3 _movementInput;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        // don't allow actions if game paused
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
            _rigidbody.velocity = _movementInput * moveSpeed;
        }
        else
        {
            // If no movement keys are pressed, stop the player
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
