using UnityEngine;
using System.Collections;
using System;

public class EnergyRecoveryBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("EnergyRecovery", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);
        ((CannonTowerMono)unit).MpAdd(attack*Value_0);
    }
}
