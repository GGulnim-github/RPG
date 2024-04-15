using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent OnReciveDamage;

    public void ReciveDamage(uint damage)
    {
        Debug.Log($"Damage = {damage}");
    }
}
