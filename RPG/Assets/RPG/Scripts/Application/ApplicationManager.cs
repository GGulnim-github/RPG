using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ApplicationManager : Manager<ApplicationManager>
{
    [ReadOnly][SerializeField] ApplicationInfo _info = new();
    ApplicationSettingSystem _settingSystem = new();

    public string Version { get { return _info.version; } }
    public RuntimePlatform Platform { get { return _info.platform; } }
    public DeviceType DeviceType { get { return _info.deviceType; } }

    public ApplicationTargetFrame TargetFrame
    {
        get { return _settingSystem.TargetFrame; }
        set { _settingSystem.SetTargetFrameRate(value); }
    }

    public ApplicationGraphicsQuality GraphicsQuality
    {
        get { return _settingSystem.GraphicsQuality; }
        set { _settingSystem.SetGraphicsQuality(value); }
    }

    public override void Initialize()
    {
        _info.Initialize();
        _settingSystem.Initialize();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
