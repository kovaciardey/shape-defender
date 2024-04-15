using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float baseDamage = 2f;

    public ParticleSystem bulletTrail;

    public Light bulletLight;

    public DamageType DamageType { get; set; } = DamageType.None;

    private MeshRenderer _meshRenderer;

    public void Start()
    {
        bulletTrail.Play();

        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetType(DamageType type, Color color)
    {
        Debug.Log("SET BULLET TYPE");
        
        DamageType = type;
        
        // the particle trail cannot be coloured due to a lot of reasons
        // _meshRenderer.material.color = color;
        // ParticleSystem.MainModule main = bulletTrail.main;
        // main.startColor = new ParticleSystem.MinMaxGradient(color);

        bulletLight.color = color;
    }
}
