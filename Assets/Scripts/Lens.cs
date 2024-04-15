using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lens : MonoBehaviour
{
    public Color color;

    [SerializeField]
    public DamageType damageType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.GetComponent<Bullet>().SetType(damageType, color);
        }
    }
}
