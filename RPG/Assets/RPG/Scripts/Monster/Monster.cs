using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
    public MonsterData data;
    public MonsterHUD hud;

    uint _currentHP;
    uint CurrentHP
    {
        get { return _currentHP; }
        set
        {
            _currentHP = value;
            hud.hpSlider.value = _currentHP;
            if (_currentHP == 0)
            {
                Debug.Log("Die");
            }
        }
    }

    private void Awake()
    {
        Initialize();
        hud.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        hud.Initialize(data.Level, data.Name, data.HP);
        _currentHP = data.HP;
    }

    public void ReciveDamage(uint damage)
    {
        hud.gameObject.SetActive(true);

        if (hud.damageTransform != null)
        {
            HUDManager.Instance.PlayDamageText(hud.damageTransform, damage);
        }

        if (damage >= CurrentHP)
        {
            CurrentHP = 0;
        }
        else
        {
            CurrentHP -= damage;
        }
    }
}
