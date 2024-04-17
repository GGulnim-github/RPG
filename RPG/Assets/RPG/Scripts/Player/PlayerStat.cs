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

            UIPlayerManager ui = UIPlayerManager.Instance;
            ui.UpdateLevelText(_level);
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

            UIPlayerManager ui = UIPlayerManager.Instance;
            ui.UpdateMaxStat(PlayerStatType.HP, _maxHP);
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

            UIPlayerManager ui = UIPlayerManager.Instance;
            ui.UpdateCurrentStat(PlayerStatType.HP, _currentHP);
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

            UIPlayerManager ui = UIPlayerManager.Instance;
            ui.UpdateMaxStat(PlayerStatType.MP, _maxMP);
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

            UIPlayerManager ui = UIPlayerManager.Instance;
            ui.UpdateCurrentStat(PlayerStatType.MP, _currentMP);
        }
    }

    [SerializeField] uint _maxEXP;
    public uint MaxEXP
    {
        get { return _maxEXP; }
        set
        {
            _maxEXP = value;

            UIPlayerManager ui = UIPlayerManager.Instance;
            ui.UpdateMaxEXP(_maxEXP);
        }
    }

    [SerializeField] uint _currentEXP;
    public uint CurrentEXP
    {
        get { return _currentEXP; }
        set
        {
            _currentEXP = value;

            UIPlayerManager ui = UIPlayerManager.Instance;
            if (Level == DataManager.Instance.MaxLevel)
            {
                ui.UpdateCurrentEXP(_maxEXP);
                ui.UpdateEXPPercentageText(EXPPercent);
            }
            else
            {
                ui.UpdateCurrentEXP(_currentEXP);
                ui.UpdateEXPPercentageText(EXPPercent);
            }
        }
    }

    public float EXPPercent
    {
        get
        {
            float percent;
            if (Level == DataManager.Instance.MaxLevel)
            {
                percent = 1f;
            }
            else
            {
                percent = _currentEXP / (float)_maxEXP;
            }
            return percent;
        }
    }

    public uint attack;
    public uint currentAttack;

    public void Initialize(uint level, uint currentEXP)
    {
        Level = level;

        CurrentHP = MaxHP;
        CurrentMP = MaxMP;

        CurrentEXP = currentEXP;
    }

    public void UpdateMaxHP()
    {
        MaxHP = levelData.hp;
    }

    public void UpdateMaxMP()
    {
        MaxMP = levelData.mp;
    }

    public void Levelup(uint remainEXP = 0)
    {
        Level++;

        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
        
        CurrentEXP = 0;
        AddEXP(remainEXP);
    }

    public void AddEXP(uint exp)
    {
        if (Level == DataManager.Instance.MaxLevel)
        {
            return;
        }
        uint tempEXP = CurrentEXP + exp;
        if (tempEXP >= MaxEXP)
        {
            if (Level < DataManager.Instance.MaxLevel)
            {
                uint remainEXP = tempEXP - MaxEXP;
                Levelup(remainEXP);
            }
        }
        else
        {
            CurrentEXP = tempEXP;
        }
    }
}
