using UnityEngine;
using System.Collections;
using System;

public class ReplaceThisBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("ReplaceThis", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        Game.RandomBuff();
    }
}
