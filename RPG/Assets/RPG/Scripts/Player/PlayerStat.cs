using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public uint level = 1;
    public uint exp = 0;
    
    public uint maxHP = 100;
    public uint currentHP = 100;

    public uint maxMP = 100;
    public uint currentMP = 100;

    public uint attack = 10;
    public uint currentAttack = 10;

    public uint defense = 10;
    public uint currentDefense = 10;
}
