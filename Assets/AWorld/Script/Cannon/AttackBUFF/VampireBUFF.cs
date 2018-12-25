using UnityEngine;
using System.Collections;
using System;

public class VampireBUFF : CannonBUFF
{

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Vampire", typeof(CannonBuffAttrType));
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);
        float hp = unit.Attritube.GetFloat(UnitDynamicAttritubeType.Hp);

        float value = hp + attack * Value_0;

        if (value >= unit.Attritube.GetFloat(UnitStaticAttritubeType.MaxHp))
        {
            unit.Attritube.SetAttr(UnitDynamicAttritubeType.Hp, unit.Attritube.GetFloat(UnitStaticAttritubeType.MaxHp));
        }
        else
        {
            unit.Attritube.SetAttr(UnitDynamicAttritubeType.Hp, value);
        }

        ((CannonTowerMono)unit)._PlayerStateBarContorl.SetHpBarLength(unit.Attritube.GetFloat(UnitDynamicAttritubeType.Hp) / unit.Attritube.GetFloat(UnitStaticAttritubeType.MaxHp));
    }
}
