using UnityEngine;
using System.Collections;
using System;

public class LightningBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Lightning", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float rand = UnityEngine.Random.Range(0, 1f);

        if (rand <= Value_0)
        {
            float attack = unit.Attritube.GetFloat(UnitStaticAttritubeType.Attack);

            float attackAdd = attack * Value_1 - attack;

            unit.Attritube.SetAttr(UnitDynamicAttritubeType.AttackAdd, attackAdd);

            StartCoroutine(WaitAUpdata(unit, attackAdd));

            StartCoroutine(recover(target));
        }
    }

    IEnumerator WaitAUpdata(UnitMonoBehaciour unit, float defualt)
    {
        yield return 0;
        float more = unit.Attritube.GetFloat(UnitDynamicAttritubeType.AttackAdd);
        unit.Attritube.SetAttr(UnitDynamicAttritubeType.AttackAdd, more - defualt);
    }

    IEnumerator recover(UnitMonoBehaciour unit)
    {
        float Value_2 = Attribute.GetFloat(CannonBuffAttrType.Value_2);
        UnitMove move = unit.gameObject.GetComponent<UnitMove>();
        float speed = move.MoveSpeed;
        move.MoveSpeed = 0;
        yield return new WaitForSeconds(Value_2);
        move.MoveSpeed = speed;
    }
}
