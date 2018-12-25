using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    SmallDcr,
    BigDcr
}

public class UnitManage{


    public static GameObject CreateUnit(UnitType Name,Transform Parent,Vector3 Postion)
    {
        GameObject unit= GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Unit/"+Enum.GetName(typeof(UnitType),Name)));

        if (Parent != null)
        {
            unit.transform.parent = Parent;
        }

        unit.transform.position = Postion;
        return unit;
    }


}

