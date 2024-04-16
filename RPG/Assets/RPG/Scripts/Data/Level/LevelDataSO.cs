using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataSO : ScriptableObject
{
    public SerializableDictionary<uint, LevelData> data = new();
}
