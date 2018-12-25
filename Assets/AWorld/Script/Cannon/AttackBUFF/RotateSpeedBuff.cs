using UnityEngine;
using System.Collections;
using System;

public class RotateSpeedBuff : CannonBUFF
{

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("RotateSpeed", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        unit.GetComponent<UnitMove>().RotaSpeed = Value_0;
    }
}
