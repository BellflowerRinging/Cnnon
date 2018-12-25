using UnityEngine;
using System.Collections;
using System;

public class AngerBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Anger", type);
    }


    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float Attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);
        float AIdel = unit.Attritube.GetFloat(UnitStaticAttritubeType.AttackSpeed);
        
        unit.Attritube.SetAttr(UnitStaticAttritubeType.Attack, Attack * (1 + Value_0));
        unit.Attritube.SetAttr(UnitStaticAttritubeType.AttackSpeed, AIdel * (1 - Value_1));
    }


}
