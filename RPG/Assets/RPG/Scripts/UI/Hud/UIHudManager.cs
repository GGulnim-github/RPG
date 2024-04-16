using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UIHudManager : Manager<UIHudManager>
{
    IObjectPool<UIDamageText> _damageTextPool;
   
    public override void Initialize()
    {
        _damageTextPool = new ObjectPool<UIDamageText>(CreateDamageText, OnGetDamageText, OnReleaseDamageText, OnClearDamageText);
    }

    public void PlayDamageText(Transform parent, uint damage)
    {
        UIDamageText uiDamage = _damageTextPool.Get();
        uiDamage.Play(parent, damage);
    }

    public void ReleaseDamageText(UIDamageText damage)
    {
        _damageTextPool.Release(damage);
    }

    #region Damage Pool
    UIDamageText CreateDamageText()
    {
        GameObject gameObject = Resources.Load<GameObject>($"UI/Hud/UIDamage");
        UIDamageText damage = Instantiate(gameObject).GetComponent<UIDamageText>();
        damage.Initialize();
        return damage;
    }

    void OnGetDamageText(UIDamageText damage)
    {
        damage.gameObject.SetActive(true);
    }

    void OnReleaseDamageText(UIDamageText damage)
    {
        damage.gameObject.SetActive(false);
    }

    void OnClearDamageText(UIDamageText damage)
    {
        Destroy(damage.gameObject);
    }
    #endregion
}
