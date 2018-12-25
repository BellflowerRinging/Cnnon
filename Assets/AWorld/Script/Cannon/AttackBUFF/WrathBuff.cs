using UnityEngine;
using System.Collections;
using System;

public class WrathBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Wrath", type);
    }

    float _moreAttack;
    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);
        _moreAttack = attack * (Value_0-1);
        unit.Attritube.SetAttr(UnitDynamicAttritubeType.AttackAdd, _moreAttack);
        StartCoroutine(recover(unit));
    }

    IEnumerator recover(UnitMonoBehaciour unit)
    {
        yield return new WaitForSeconds(Value_1);
        float attack = unit.Attritube.GetFloat(UnitDynamicAttritubeType.AttackAdd);
        unit.Attritube.SetAttr(UnitStaticAttritubeType.Attack, attack - _moreAttack);
        //GameObject.Destroy(this);
    }
}
