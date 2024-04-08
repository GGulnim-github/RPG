using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : Manager<LogManager>
{
    public override void Initialize()
    {

    }

    public void OnEnable()
    {
        Application.logMessageReceived += LogMessageAction;
    }

    public void OnDisable()
    {
        Application.logMessageReceived -= LogMessageAction;
    }

    void LogMessageAction(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Warning || type == LogType.Error || type == LogType.Assert || type == LogType.Exception)
        {
            // TODO : ��� �޼����� ���� �޼����� ��µɶ� ������ �α׸� ������.
        }
    }
}
