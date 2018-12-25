using UnityEngine;
using System.Collections;
using System;

public class EssenceOfLifeBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("EssenceOfLife", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float maxhp = unit.Attritube.GetFloat(UnitStaticAttritubeType.MaxHp) * Value_0;

        unit.Attritube.SetAttr(UnitStaticAttritubeType.MaxHp, maxhp);
        unit.Attritube.SetAttr(UnitDynamicAttritubeType.Hp, maxhp);

        ((CannonTowerMono)unit)._PlayerStateBarContorl.SetHpBarLength(1f);
    }
}
