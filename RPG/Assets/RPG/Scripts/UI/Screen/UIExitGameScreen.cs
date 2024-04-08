using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExitGameScreen : UIScreen
{
    private void OnEnable()
    {
        SoundManager.Instance.PlayUI("Bubble Pop");
    }

    public override void OnClickBackButton()
    {
        base.OnClickBackButton();
        UIManager.Instance.OpenUI<UITitleScreen>();
    }
}
