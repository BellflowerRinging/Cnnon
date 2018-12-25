using UnityEngine;
using System.Collections;
using System;

public class SmallDcrMono : UnitMonoBehaciour
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("SmallDcr", typeof(UnitStaticAttritubeType));
    }
}
