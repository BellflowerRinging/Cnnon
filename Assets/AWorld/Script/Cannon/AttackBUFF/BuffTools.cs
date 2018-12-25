using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffTools
{
    public static Collider[] GetOverlapSphere(Vector3 pos, float r)
    {
        return Physics.OverlapSphere(pos, r);
    }

    public static List<UnitMonoBehaciour> GetOverlapSphereUnit(Vector3 pos, float r)
    {
        Collider[] collider = GetOverlapSphere(pos, r);
        List<UnitMonoBehaciour> list = new List<UnitMonoBehaciour>();
        for (int i = 0; i < collider.Length; i++)
        {
            UnitMonoBehaciour unitMono;
            if ((unitMono = collider[i].gameObject.GetComponent<UnitMonoBehaciour>()) != null)
            {
                list.Add(unitMono);
            }
        }
        return list;
    }

}
