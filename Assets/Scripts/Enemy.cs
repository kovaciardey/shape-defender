using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DamageType
{
    Red = 0,
    Green = 1,
    Blue = 2,
    None = 3
}

public class Enemy : MonoBehaviour
{
    // I could experiment with variable movement speed
    public float speed = 5f; // Adjust speed as needed
    public HealthBar healthBar;

    public float maxLife = 10f; 
    public float currentLife;

    public float damageToPlayer = 5f; 
    
    // this is high to make sure when same colour it's just one shot kill 
    // without having to rework the damage script
    [Range(1f, 3f)]
    public float extraDamageCoefficient = 1.5f; 
    [Range(0.25f, 1f)]
    public float lowerDamageCoefficient = 0.5f;
    
    public Color[] possibleColors = {
        Color.red, Color.green, Color.blue
    };

    public float avoidanceRadius = 1f;
    public float avoidanceWeight = 1f;

    public float stunDuration = 0.5f;
    public Color damagedColor;

    public ParticleSystem destroyedParticles;
    
    private Transform _player;
    private DamageType _enemyType;
    private MeshRenderer _meshRenderer;

    private Color _selectedColor;

    private bool _canMove = true;
    private bool _isDestroyed = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has "Player" tag
        _meshRenderer = GetComponent<MeshRenderer>();
        
        SetMonsterType();
        _meshRenderer.material.color = _selectedColor;
        
        // reset the maxlife of the enemy
        currentLife = maxLife;
        healthBar.UpdateMaximumValue(maxLife);
    }

    void Update()
    {
        healthBar.SetValue(currentLife);
        
        if (_isDestroyed) return;
        
        if (_canMove)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (_player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }
        
        Vector3 direction = (_player.position - transform.position).normalized;
        
        // Avoidance behavior
        Vector3 avoidanceDirection = Vector3.zero;
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, avoidanceRadius);
        foreach (Collider enemyCollider in nearbyEnemies)
        {
            if (enemyCollider.gameObject.CompareTag("Player"))
            {
                continue;
            }
            
            if (enemyCollider.gameObject.CompareTag("Bullet"))
            {
                continue;
            }

            if (enemyCollider.gameObject == gameObject)
            {
                continue;
            }
            
            Vector3 avoidanceVector = transform.position - enemyCollider.transform.position;
            avoidanceDirection += avoidanceVector.normalized / avoidanceVector.magnitude; // Add normalized vector towards enemy
        }
        
        // Combine movement direction and avoidance direction
        Vector3 combinedDirection = direction + avoidanceDirection.normalized * avoidanceWeight;
        Vector3 newPosition = transform.position + combinedDirection * (speed * Time.deltaTime);
        
        transform.position = new Vector3(newPosition.x, 0.5f, newPosition.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isDestroyed) return;
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            
            // damage calculation
            float damageToTake = bullet.baseDamage;
            
            if (bullet.DamageType == DamageType.None)
            {
                currentLife -= damageToTake; // base damage
            }
            else if (bullet.DamageType == _enemyType)
            {
                currentLife -= damageToTake * extraDamageCoefficient;
            }
            else
            {
                // not really sure about this one... will have to see
                currentLife -= lowerDamageCoefficient;
            }

            // kill enemy
            if (currentLife <= 0)
            {
                _isDestroyed = true;
                
                destroyedParticles.Play();
                // Debug.Log(_enemyType);
                Destroy(gameObject, destroyedParticles.main.duration);
                // Destroy(gameObject);
            }

            StartCoroutine(Stun());
            
            
            // destroy bullet
            Destroy(other.gameObject);
        }
    }

    private void SetMonsterType()
    {
        int index = Random.Range(0, possibleColors.Length);
        
        _selectedColor = possibleColors[index];

        switch (index)
        {
            case 0:
                _enemyType = DamageType.Red;
                break;
            case 1:
                _enemyType = DamageType.Green;
                break;
            case 2:
                _enemyType = DamageType.Blue;
                break;
        }
    }

    IEnumerator Stun()
    {
        _canMove = false;
        _meshRenderer.material.color = damagedColor;
        
        yield return new WaitForSeconds(stunDuration);

        _canMove = true;
        _meshRenderer.material.color = _selectedColor;
    }
}
