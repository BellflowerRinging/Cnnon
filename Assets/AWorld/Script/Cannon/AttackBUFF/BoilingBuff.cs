using UnityEngine;
using System.Collections;
using System;

public class BoilingBuff : CannonBUFF
{

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Boiling", type);
    }

    float _moreSpeed;
    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float speed = unit.Attritube.GetFloat(UnitStaticAttritubeType.AttackSpeed);
        _moreSpeed = speed * (1f - Value_0);
        Debug.Log(_moreSpeed);
        float speedAdd = unit.Attritube.GetFloat(UnitDynamicAttritubeType.AttackSpeedAdd);
        unit.Attritube.SetAttr(UnitDynamicAttritubeType.AttackSpeedAdd, speedAdd + _moreSpeed);
        StartCoroutine(recover(unit));
    }

    IEnumerator recover(UnitMonoBehaciour unit)
    {
        yield return new WaitForSeconds(Value_1);
        float speed = unit.Attritube.GetFloat(UnitDynamicAttritubeType.AttackSpeedAdd);
        unit.Attritube.SetAttr(UnitDynamicAttritubeType.AttackSpeedAdd, speed - _moreSpeed);
        //GameObject.Destroy(this);
    }
}
