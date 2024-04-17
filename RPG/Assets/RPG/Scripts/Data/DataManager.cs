using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Manager<DataManager>
{
    public LevelDataSO level;
    public uint MaxLevel
    {
        get { return (uint)level.data.Count; }
    }

    public override void Initialize()
    {
        level = Resources.Load<LevelDataSO>("Data/LevelData");
    }


}

