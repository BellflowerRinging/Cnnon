using UnityEngine;
using System.Collections;
using System;

public class BalanceBuff : CannonBUFF
{



    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Balance", type);
    }


    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);

        Game.SetMoney(Game.HaveMoney + attack * Value_0);

        ((CannonTowerMono)unit).MpAdd(attack * Value_1);
    }
}
