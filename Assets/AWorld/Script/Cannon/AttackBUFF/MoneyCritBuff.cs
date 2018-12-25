using UnityEngine;
using System.Collections;
using System;

public class MoneyCritBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("MoneyCrit", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        Game.SetMoney(Game.HaveMoney * Value_0);
    }
}
