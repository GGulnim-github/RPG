using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string Name;
    public uint HP;
    public uint offencse;
    public uint defense;
    
    public uint currentHP;

    private void Start()
    {
        currentHP = HP;
    }

    public void OnReciveDamage(uint damage)
    {
        currentHP -= damage;
        if (currentHP == 0)
        {
            Debug.Log("Die");
        }
    }
}
