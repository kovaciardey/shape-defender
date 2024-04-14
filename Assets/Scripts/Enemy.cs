using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Adjust speed as needed
    private Transform _player;

    public Color[] possibleColors = {
        Color.red, Color.green, Color.blue
    };

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has "Player" tag
        
        // set random colour
        gameObject.GetComponent<MeshRenderer>().material.color = possibleColors[Random.Range(0, possibleColors.Length)];
        
        // need more control for the color here to define the damage better
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (_player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }

        Vector3 direction = (_player.position - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * (speed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, 0.5f, newPosition.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // when hit with bullet just destroy both
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
