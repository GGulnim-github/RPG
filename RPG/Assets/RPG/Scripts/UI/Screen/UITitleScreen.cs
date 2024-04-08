using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitleScreen : UIScreen
{
    public void OnClickStartGame()
    {
        UIManager.Instance.Clear();
        SceneLoader.Instance.LoadScene("Demo_Scene_Puddle");
    }

    public void OnClickSetting()
    {
        base.OnClickBackButton();
        UIManager.Instance.OpenUI<UISettingScreen>();
    }

    public override void OnClickBackButton()
    {
        base.OnClickBackButton();
        UIManager.Instance.OpenUI<UIExitGameScreen>();
    }
}
