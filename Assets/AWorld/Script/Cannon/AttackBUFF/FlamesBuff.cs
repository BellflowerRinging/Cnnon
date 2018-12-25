using UnityEngine;
using System.Collections;
using System;

public class FlamesBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Flames", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float rand = UnityEngine.Random.Range(0, 1f);

        if (rand <= Value_0)
        {
            float value_2 = Attribute.GetFloat(CannonBuffAttrType.Value_2);

            float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack) * value_2;

            StartCoroutine(WaitAUpdata(target, Value_1, attack));

        }
    }


    IEnumerator WaitAUpdata(UnitMonoBehaciour unit, float time, float attack)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            if (unit == null) break;

            unit.Damage(attack);
        }
    }
}
