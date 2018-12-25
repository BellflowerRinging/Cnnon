using UnityEngine;
using System.Collections;
using System;

public class SuckMoneyBuff : CannonBUFF
{



    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("SuckMoney", type);
    }


    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        Game.SetMoney(Game.HaveMoney + unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack) * Value_0);
    }
}
