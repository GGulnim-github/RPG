using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Manager<DataManager>
{
    public LevelDataSO level;

    public override void Initialize()
    {
        level = Resources.Load<LevelDataSO>("Data/LevelData");
    }
}

