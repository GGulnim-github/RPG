using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class GameScene : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.OpenUI<UIGameScreen>();
    }
}
