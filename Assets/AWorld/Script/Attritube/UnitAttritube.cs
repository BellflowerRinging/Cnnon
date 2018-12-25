using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStaticAttritubeType
{
    Name,
    MaxHp,
    MaxMp,
    Attack,
    Level,
    AttackSpeed,
}

public enum UnitDynamicAttritubeType
{
    Hp,
    Mp,
    MaxHpAdd,
    MaxMpAdd,
    AttackAdd,
    AttackSpeedAdd,
}

public enum PlayerAttritubeType
{
    Name,
    Hp,
    MaxHp,
    Mp,
    MaxMp,
    Attack,
    Level,
    AttackSpeed
}

public class UnitAttritube : IAttritube
{

    protected Dictionary<string, object> Attr;

    protected Dictionary<string, object> DynamicAttr;

    protected Type DefaultAttritubeType;

    public UnitAttritube(Dictionary<string, object> Attr)
    {
        CheckName((string[])Attr["_TabelHeard"], typeof(UnitStaticAttritubeType));

        DynamicAttr = new Dictionary<string, object>();
    }

    public UnitAttritube(Dictionary<string, object> attr,Type type)
    {
        CheckName((string[])attr["_TabelHeard"], type);

        Attr = new Dictionary<string, object>(attr);

        DynamicAttr = new Dictionary<string, object>();
    }


    protected virtual void CheckName(string[] TabelHeard,Type type)
    {
        DefaultAttritubeType = type;

        CheckName(DefaultAttritubeType, TabelHeard);
    }

    /// <summary>
    /// 检查列名是否对应枚举名称
    /// </summary>
    /// <param name="type"></param>
    /// <param name="TabelHeard"></param>
    protected void CheckName(Type type ,string[] TabelHeard)
    {
        string[] EnumNames = Enum.GetNames(type);
        
        if (EnumNames == null || TabelHeard == null )
        {
            throw new Exception(type.Name + " == null || TabelHeard == null");
        }

        if (EnumNames.Length == 0 || TabelHeard.Length == 0)
        {
            throw new Exception(type.Name + ".Length == 0 || TabelHeard.Length == 0");
        }

        if (EnumNames.Length != TabelHeard.Length)
        {
            throw new Exception(string.Format(type.Name+ ".Length={0} != TabelHeard.Length={1}", EnumNames.Length, TabelHeard.Length));
        }

        List<String> EName = new List<string>(EnumNames);
        List<String> THeard = new List<string>(TabelHeard);
        List<string> res = new List<string>();


        foreach (var item in THeard) if (!EName.Contains(item)) res.Add(type.Name + " miss '" + item + "'");

        foreach (var item in EName) if (!THeard.Contains(item)) res.Add("THeard miss '" + item + "'");

        if (res.Count!=0)
        {
            string str = "";
            foreach (var item in res) str += item + ",";
            throw new Exception(str);
        }

        /*for (int i = 0; i < TabelHeard.Length; i++)
        {
            if (EnumNames[i] != TabelHeard[i])
            {
                throw new Exception(DefaultAttritubeType.Name + " != TabelHeard Index:" + i);
            }
        }*/
    }

    protected object Get(Enum type)
    {
        string name = Enum.GetName(type.GetType(), type);

        if (Attr.ContainsKey(name))
        {
            return Attr[name];
        }
        else if (DynamicAttr.ContainsKey(name))
        {
            return DynamicAttr[name];
        }
        else
        {
            return null;
        }

    }

    protected void Set(Enum type, object obj)
    {
        string name = Enum.GetName(type.GetType(), type);
        
        if (Attr.ContainsKey(name))
        {
            Attr[name] = obj;

            return;
        }

        if (DynamicAttr.ContainsKey(name))
        {
            DynamicAttr[name] = obj;
        }
        else
        {
            DynamicAttr.Add(name,obj);
        }

    }

    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <typeparam name="T">欲返回类型 float、int、string以及bool</typeparam>
    /// <param name="type">对应的属性种类</param>
    /// <returns></returns>
    public object GetAttr(Enum type)
    {
        return Get(type);
    }

    /// <summary>
    /// 设定属性值
    /// </summary>
    /// <typeparam name="T">设定的属性类型</typeparam>
    /// <param name="type">对应的属性种类</param>
    /// <param name="obj">属性</param>
    public void SetAttr(Enum type, object obj)
    {
        Set(type, obj);
    } 


    public string GetString(Enum type)
    {
        return Get(type).ToString();
    }
    public float GetFloat(Enum type)
    {
        object result = Get(type);
        if (result != null)
        {
            return float.Parse(Get(type).ToString());
        }
        else
        {
            return 0f;
        }
    }
    public int GetIntger(Enum type)
    {
        object result = Get(type);
        if (result != null)
        {
            return int.Parse(Get(type).ToString());
        }
        else
        {
            return 0;
        }
    }
    public bool GetBool(Enum type)
    {
        object result = Get(type);
        if (result != null)
        {
            return bool.Parse(Get(type).ToString());
        }
        else
        {
            return false;
        }

    }

    //猜测子类继承UnitAttritube 也拥有Equals的功能
    public override bool Equals(object other)
    {
        if (other.GetType() != this.Attr.GetType()) return false;

        Dictionary<string, object> B = (Dictionary<string, object>)other;

        if (this.Attr == null && B == null) return true;

        if (this.Attr == null || B == null) return false;

        if (this.Attr.Count != B.Count) return false;

        foreach (var dir in this.Attr)
        {
            if (!B.ContainsKey(dir.Key)) return false;
            else if (B[dir.Key] != dir.Value) return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        var hashCode = -1865120558;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<string, object>>.Default.GetHashCode(Attr);
        return hashCode;
    }

}
