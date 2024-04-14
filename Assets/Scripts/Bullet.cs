using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float baseDamage = 2f;

    public DamageType DamageType { get; set; } = DamageType.None;
}
