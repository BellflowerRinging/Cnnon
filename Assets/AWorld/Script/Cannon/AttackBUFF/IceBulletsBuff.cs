using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class IceBulletsBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("IceBullets", type);
    }

    List<UnitMove> list = new List<UnitMove>();

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        Color color = new Color(0, 192f / 255f, 1f);
        foreach (var targetCol in BuffTools.GetOverlapSphereUnit(target.transform.position,Value_0))
        {
            UnitMove move = targetCol.gameObject.GetComponent<UnitMove>();
            if (!list.Contains(move))
            {
                move.MoveSpeed *= (1f-Value_1);
                targetCol._DefCol = color;
                targetCol._Renderer.material.color = color;
                list.Add(move);
            }
        }
    }

    public override void Add(CannonBUFF buff)
    {
        if (Value_0<=50)
        {
            Value_0 += 10;
        }
        else
        {
            Game.UISelectBuffContorl.BuffList.Remove(buff);
        }
    }
}
