using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public Transform damagePos;

    public UnityAction<uint> OnReciveDamageAction;

    public void ReciveDamage(uint damage)
    {
        if (damagePos != null)
        {
            UIHudManager.Instance.PlayDamageText(damagePos, damage);
        }

        PlayerController.Instance.AddExp(damage * 40);
        OnReciveDamageAction?.Invoke(damage);
    }
}
