using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullet")]
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;

    private ActionController _ac;

    private void Start()
    {
        _ac = GetComponent<ActionController>();
    }

    void Update()
    {
        // if left click
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        
        // it might be an idea to move this on a bullet script
        
        // Get the Rigidbody component of the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Apply velocity to the bullet in the calculated direction
        bulletRb.velocity = _ac.GetDirection().normalized * bulletSpeed;
        
        Destroy(bullet, bulletLifetime);
    }
}
