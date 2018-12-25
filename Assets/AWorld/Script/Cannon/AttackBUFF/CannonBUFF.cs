using UnityEngine;
using System;

public class CannonBUFF : MonoBehaviour
{
    public string Name { get; protected set; }

    public string ChinName { get; protected set; }

    public string Introduce { get; protected set; }

    public IAttritube Attribute { get; protected set; }

    public bool Immediately = true;

    public bool Repeat = true;

    public float Value_0 = 0;

    public float Value_1 = 0;


    public CannonGameContorl Game;

    private void Start()
    {
        Attribute = LoadAttritube("NULL", typeof(CannonBuffAttrType));

        InitBuff();

        Game = GameObject.Find("GameContorl").GetComponent<CannonGameContorl>();
    }

    protected virtual void InitBuff()
    {
        Name = Attribute.GetString(CannonBuffAttrType.Name);

        ChinName = Attribute.GetString(CannonBuffAttrType.ChinName);

        Introduce = Attribute.GetString(CannonBuffAttrType.Introduce);

        Repeat = Attribute.GetBool(CannonBuffAttrType.Repeat);

        Immediately = Attribute.GetBool(CannonBuffAttrType.Immediately);

        Value_0 = Attribute.GetFloat(CannonBuffAttrType.Value_0);

        Value_1 = Attribute.GetFloat(CannonBuffAttrType.Value_1);
    }

    public virtual void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target){ }

    protected virtual IAttritube LoadAttritube(string AttrName, Type type)
    {
        return new UnitAttritube(AttritubeAccess.GetAttrDic(AttrName), type);
    }

    public virtual void Add(CannonBUFF buff)
    {
        this.Value_0 += buff.Value_0;
        this.Value_1 += buff.Value_1;
    }


}

struct a
{
    
}


public enum CannonBuffAttrType
{
    Name,
    ChinName,
    Introduce,
    Immediately,
    Repeat,
    Value_0,
    Value_1,
    Value_2,

}