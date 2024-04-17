using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HUDManager : Manager<HUDManager>
{
    IObjectPool<HUDDamageText> _damageTextPool;
   
    public override void Initialize()
    {
        _damageTextPool = new ObjectPool<HUDDamageText>(CreateDamageText, OnGetDamageText, OnReleaseDamageText, OnClearDamageText);
    }

    public void PlayDamageText(Transform parent, uint damage)
    {
        HUDDamageText uiDamage = _damageTextPool.Get();
        uiDamage.Play(parent, damage);
    }

    public void ReleaseDamageText(HUDDamageText damage)
    {
        _damageTextPool.Release(damage);
    }

    #region Damage Pool
    HUDDamageText CreateDamageText()
    {
        GameObject gameObject = Resources.Load<GameObject>($"HUD/HUDDamageText");
        HUDDamageText damage = Instantiate(gameObject).GetComponent<HUDDamageText>();
        damage.Initialize();
        return damage;
    }

    void OnGetDamageText(HUDDamageText damage)
    {
        damage.gameObject.SetActive(true);
    }

    void OnReleaseDamageText(HUDDamageText damage)
    {
        damage.gameObject.SetActive(false);
    }

    void OnClearDamageText(HUDDamageText damage)
    {
        Destroy(damage.gameObject);
    }
    #endregion
}
