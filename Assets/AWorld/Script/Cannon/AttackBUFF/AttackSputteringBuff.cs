using UnityEngine;
using System.Collections;
using System;

public class AttackSputteringBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("AttackSputtering", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);

        foreach (var unitMono in BuffTools.GetOverlapSphereUnit(target.gameObject.transform.position,Value_0))
        {
            if (unitMono != target && unitMono != unit)
            {
                float dis = Vector3.Distance(unitMono.transform.position, target.transform.position);
                dis = Mathf.Clamp(dis, 1, Value_0-1);
                float damage = attack * Value_1 * ((Value_0 - dis) / Value_0);
                unitMono.Damage(damage);
                ((CannonTowerMono)unit).BoomParcitle(unitMono.gameObject, damage);
            }
        }
    }
}
