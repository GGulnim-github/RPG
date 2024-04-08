using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ApplicationInfo
{
    public string version;
    public RuntimePlatform platform;
    public DeviceType deviceType;

    public void Initialize()
    {
        version = Application.version;
        platform = Application.platform;
        deviceType = SystemInfo.deviceType;
    }
}
