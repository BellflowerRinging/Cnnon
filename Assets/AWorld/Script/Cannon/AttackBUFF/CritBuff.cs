using UnityEngine;
using System.Collections;
using System;

public class CritBuff : CannonBUFF
{


    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Crit", typeof(CannonBuffAttrType));
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float Damage = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);

        if (UnityEngine.Random.Range(0, 1f) <= Value_0)
        {
            float more = Damage * Value_1 -Damage;
            unit.Attritube.SetAttr(UnitDynamicAttritubeType.AttackAdd, more);
            StartCoroutine(WaitAUpdata(unit, more));
        }
    }

    IEnumerator WaitAUpdata(UnitMonoBehaciour unit, float defualt)
    {
        yield return 0;
        float Damage = unit.Attritube.GetFloat(UnitDynamicAttritubeType.AttackAdd);
        unit.Attritube.SetAttr(UnitDynamicAttritubeType.AttackAdd, Damage - defualt);
    }

    public override void Add(CannonBUFF buff)
    {
        if (Value_0 > 1f)
        {
            Value_1 += Value_0 - 1f;
        }
        else
        {
            Value_0 += buff.Value_0;
        }
    }

}
