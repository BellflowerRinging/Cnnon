using UnityEngine;
using System.Collections;
using System;

public class RandomMoneyBuff : CannonBUFF
{

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("RandomMoney", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);
        float money = attack * UnityEngine.Random.Range(Value_0,Value_1);
        Game.SetMoney(Game.HaveMoney+money);
    }
}
