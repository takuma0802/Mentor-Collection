using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

[Serializable]
public class User
{

    [SerializeField] private IntReactiveProperty _money ;
    [SerializeField] private List<Character> _characters;
    
    public ReadOnlyReactiveProperty<int>  Money
    {
        get { return _money.ToReadOnlyReactiveProperty();}
    }
    
    public List<Character> Characters
    {
        get { return _characters ?? (_characters = new List<Character>()); }
    }

    public int ProductivityPerTap
    {
        get
        {
            int sum = _characters.Sum(c => c.Power);
            return (sum == 0) ? 1 : sum;
        }
    }

    public void AddMoney(int cost)
    {
        _money.Value += cost;
    }

    public void ConsumptionLevelUpCost(int cost)
    {
        _money.Value -= cost;
    }

    public Character NewCharacter(MstCharacter data)
    {
        var uniqueId = (Characters.Count == 0) ? 1 : _characters[_characters.Count - 1].UniqueId + 1;
        var chara = new Character(uniqueId, data);
        Characters.Add(chara);
        _money.Value -= data.InitialCost;
        return chara;

    }
}
