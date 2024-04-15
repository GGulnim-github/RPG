using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public uint damage = 1;
    public Vector3 offset;
    public float radius;

    public LayerMask targetLayers;

    RaycastHit[] _raycastHits = new RaycastHit[32];

    public void Attack()
    {
        Attack(offset, radius);
    }

    void Attack(Vector3 offset, float radius)
    {
        Vector3 worldPos = transform.position + transform.TransformVector(offset);
        Vector3 attackVector = Vector3.forward * 0.0001f;

        Ray ray = new Ray(worldPos, attackVector.normalized);

        int contacts = Physics.SphereCastNonAlloc(ray, radius, _raycastHits, attackVector.magnitude, targetLayers, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < contacts; i++)
        {
            Collider col = _raycastHits[i].collider;

            if (col != null)
                CheckDamage(col);
        }
    }

    void CheckDamage(Collider other)
    {
        Damageable d = other.GetComponent<Damageable>();
        
        if (d == null)
        {
            return;
        }

        d.ReciveDamage(damage);
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Vector3 worldPos = transform.position + transform.TransformVector(offset);
        Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
        Gizmos.DrawSphere(worldPos, radius);
    }

#endif
}
