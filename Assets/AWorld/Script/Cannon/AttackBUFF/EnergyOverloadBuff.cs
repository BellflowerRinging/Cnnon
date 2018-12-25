using UnityEngine;
using System;

public class EnergyOverloadBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("EnergyOverload", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        Game.SingelKillData.SingelDcrMp+=Value_0;
    }





}