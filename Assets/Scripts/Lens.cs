using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lens : MonoBehaviour
{
    public Color color;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
