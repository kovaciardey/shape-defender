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

    private Vector3 _aimDirection;
    
    void Update()
    {
        Aim();
        
        // if left click
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Aim()
    {
        // Get the position of the mouse in screen coordinates
        Vector3 mousePosition = Input.mousePosition;
        
        // Convert the screen point to a point in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

        // Calculate the direction from the current object's position to the mouse position
        _aimDirection = mouseWorldPosition - transform.position;
        _aimDirection.y = 0; // Optional: if you want to restrict rotation to the X-Z plane

        // Rotate the object to face the mouse direction
        if (_aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_aimDirection);
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
        bulletRb.velocity = _aimDirection.normalized * bulletSpeed;
        
        Destroy(bullet, bulletLifetime);
    }

    public Vector3 GetDirection()
    {
        return _aimDirection;
    }
}
