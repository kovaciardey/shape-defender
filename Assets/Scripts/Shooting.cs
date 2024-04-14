using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [Header("Bullet")]
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;

    [Header("Shooting")] 
    public float fireRate = 0.4f;
    public bool usesAmmo = true;
    
    public Text ammoClipText;
    public ReloadBar reloadBar;


    private ActionController _ac;
    private AmmoController _ammoController;
    
    private bool _canFire;

    private void Start()
    {
        _ac = GetComponent<ActionController>();
        _ammoController = GetComponent<AmmoController>();
        
        reloadBar.SetMaxReloadValue(_ammoController.reloadTime);

        _canFire = true;
    }

    void Update()
    {
        UpdateAmmoDisplay();
        UpdateReloadBarDisplay();
        
        // if left click
        if (Input.GetMouseButtonDown(0) && _canFire && !_ammoController.IsReloading)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        StartCoroutine(FireRate());
        
        // Debug.Log("Shoot");

        IEnumerator FireRate()
        {
            _canFire = false;

            if (GetComponent<AmmoController>().CurrentClipAmmo > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        
                // it might be an idea to move this on a bullet script
        
                // Get the Rigidbody component of the bullet
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                // Apply velocity to the bullet in the calculated direction
                bulletRb.velocity = _ac.GetDirection().normalized * bulletSpeed;
        
                Destroy(bullet, bulletLifetime);
                
                if (usesAmmo)
                {
                    gameObject.GetComponent<AmmoController>().SubtractAmmo();
                }

                yield return new WaitForSeconds(fireRate);
                _canFire = true;
            }
        }
    }
    
    // show ammo
    private void UpdateAmmoDisplay()
    {
        ammoClipText.text = _ammoController.CurrentClipAmmo.ToString();
    }
    
    private void UpdateReloadBarDisplay()
    {
        reloadBar.SetCurrentReloadValue(_ammoController.CurrentReloadTime);
    } 
}
