using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : PersistentSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        LogManager.Instance.Initialize();
        ApplicationManager.Instance.Initialize();
        SoundManager.Instance.Initialize();
        UIManager.Instance.Initialize();
        UIHudManager.Instance.Initialize();
        DataManager.Instance.Initialize();
    }
}
