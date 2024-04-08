using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingScreen : UIScreen
{
    public override void OnClickBackButton()
    {
        base.OnClickBackButton();
        UIManager.Instance.OpenUI<UITitleScreen>();
    }
}
