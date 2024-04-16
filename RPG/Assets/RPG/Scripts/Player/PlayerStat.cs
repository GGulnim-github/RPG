using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public LevelData levelData;

    [SerializeField] uint _level;
    public uint Level
    {
        get { return _level; }
        set
        {
            _level = value;
            levelData = DataManager.Instance.level.data[_level];

            UpdateMaxHP();
            UpdateMaxMP();
            MaxEXP = levelData.exp;
        }
    }

    [SerializeField] uint _maxHP;
    public uint MaxHP
    {
        get 
        {
            return _maxHP;
        }
        set
        {
            _maxHP = value;

            if (_currentHP > _maxHP)
            {
                CurrentHP = _maxHP;
            }
            
            UIGameScreen gameScreen = UIManager.Instance.GetUI<UIGameScreen>();
            if (gameScreen != null )
            {
                gameScreen.hpSlider.SetMaxValue(_maxHP);
            }
        }
    }

    [SerializeField] uint _currentHP;
    public uint CurrentHP
    {
        get { return _currentHP; }
        set
        {
            if (_currentHP > _maxHP)
            {
                _currentHP = _maxHP;
            }

            _currentHP = value;

            UIGameScreen gameScreen = UIManager.Instance.GetUI<UIGameScreen>();
            if (gameScreen != null)
            {
                gameScreen.hpSlider.SetValue(_currentHP);
            }
        }
    }

    [SerializeField] uint _maxMP;
    public uint MaxMP
    {
        get { return _maxMP; }
        set
        {
            _maxMP = value;

            if (_currentMP > _maxMP)
            {
                CurrentMP = _maxMP;
            }

            UIGameScreen gameScreen = UIManager.Instance.GetUI<UIGameScreen>();
            if (gameScreen != null)
            {
                gameScreen.mpSlider.SetMaxValue(_maxMP);
            }
        }
    }

    [SerializeField] uint _currentMP;
    public uint CurrentMP
    {
        get { return _currentMP; }
        set
        {
            if (_currentMP > _maxMP)
            {
                _currentMP = _maxMP;
            }

            _currentMP = value;

            UIGameScreen gameScreen = UIManager.Instance.GetUI<UIGameScreen>();
            if (gameScreen != null)
            {
                gameScreen.mpSlider.SetValue(_currentMP);
            }
        }
    }

    [SerializeField] uint _maxEXP;
    public uint MaxEXP
    {
        get { return _maxEXP; }
        set
        {
            _maxEXP = value;

            UIGameScreen gameScreen = UIManager.Instance.GetUI<UIGameScreen>();
            if (gameScreen != null)
            {
            }
        }
    }

    [SerializeField] uint _currentEXP;
    public uint CurrentEXP
    {
        get { return _currentEXP; }
        set
        {
            _currentEXP = value;

            UIGameScreen gameScreen = UIManager.Instance.GetUI<UIGameScreen>();
            if (gameScreen != null)
            {
            }
        }
    }

    public uint attack = 10;
    public uint currentAttack = 10;

    public void Initialize(uint level)
    {
        Level = level;
    }

    public void UpdateMaxHP()
    {
        MaxHP = levelData.hp;
    }

    public void UpdateMaxMP()
    {
        MaxMP = levelData.mp;
    }
}
