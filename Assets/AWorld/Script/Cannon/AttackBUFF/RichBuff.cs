using UnityEngine;
using System.Collections;
using System;

public class RichBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Rich", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        Game.SingelKillData.SingelDcrMoney *= (1f+Value_0);
    }
}
