using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Adjust speed as needed
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has "Player" tag
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
        transform.position += direction * (speed * Time.deltaTime);
    }
}
