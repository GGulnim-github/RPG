using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public uint HP;
    public uint MP;
    public uint EXP;

    public LevelData(uint hp, uint mp, uint exp)
    {
        this.HP = hp;
        this.MP = mp;
        this.EXP = exp;
    }
}
