using UnityEngine;
using System.Collections;
using System;

public interface IAttritube 
{
    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <typeparam name="T">欲返回类型 float、int、string以及bool</typeparam>
    /// <param name="type">对应的属性种类</param>
    /// <returns></returns>
    object GetAttr(Enum type);

    /// <summary>
    /// 设定属性值
    /// </summary>
    /// <typeparam name="T">设定的属性类型</typeparam>
    /// <param name="type">对应的属性种类</param>
    /// <param name="obj">属性</param>
    void SetAttr(Enum type, object obj);


    string GetString(Enum type);
    float GetFloat(Enum type);
    int GetIntger(Enum type);
    bool GetBool(Enum type);
    //猜测子类继承UnitAttritube 也拥有Equals的功能
    bool Equals(object other);
}
