using UnityEngine;
using System.Collections;
using System;

public class BigDcrMono : UnitMonoBehaciour
{
    CannonGameContorl Game;

    public override void InitUnitMonoBehaciour()
    {
        base.InitUnitMonoBehaciour();

        Game = GameObject.Find("GameContorl").GetComponent<CannonGameContorl>();
    }

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("BigDcr", typeof(UnitStaticAttritubeType));
    }

    public override void Death()
    {
        base.Death();

        Game.DcrDie();
    }
}
