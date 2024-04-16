using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public uint hp;
    public uint mp;
    public uint exp;

    public LevelData(uint hp, uint mp, uint exp)
    {
        this.hp = hp;
        this.mp = mp;
        this.exp = exp;
    }
}
