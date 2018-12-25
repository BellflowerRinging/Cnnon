using UnityEngine;
using System.Collections;
using System;

public class RigidityBuff : CannonBUFF
{

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Rigidity", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        if (Game.SingelKillData.SingelDcrDamageMult >= 0.5f)
        {
            Game.SingelKillData.SingelDcrDamageMult -= Value_0;
        }
        else
        {
            Game.SetMoney(Game.HaveMoney + Value_1);
        }
    }
}
