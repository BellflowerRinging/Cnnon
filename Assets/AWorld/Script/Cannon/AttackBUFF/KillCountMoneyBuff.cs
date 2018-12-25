using UnityEngine;
using System.Collections;
using System;

public class KillCountMoneyBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("KillCountMoney", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float money = UnityEngine.Random.Range(Game.KillCount * Value_0, Game.KillCount * Value_1);

        Game.SetMoney(Game.HaveMoney+money);
    }
}
