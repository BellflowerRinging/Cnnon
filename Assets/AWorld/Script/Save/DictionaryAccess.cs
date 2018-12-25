using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryAccess
{
    /// <summary>
    /// 写入字典数据到PlayerPrefs
    /// </summary>
    /// <param name="DicName">字典名称</param>
    /// <param name="dictionary">预写入的字典</param>
    public static void SaveDictionary(string DicName, Dictionary<string, string> dictionary)
    {


        JsonData json = new JsonData();

        foreach (var item in dictionary)
        {
            json[item.Key] = item.Value;
        }


        PlayerPrefs.SetString(DicName, json.ToJson());
    }

    public static void SaveDictionary(string DicName, Dictionary<string, int> dictionary)
    {
        SaveDictionary(DicName, DictionaryValueToString(dictionary));
    }
    public static void SaveDictionary(string DicName, Dictionary<string, double> dictionary)
    {
        SaveDictionary(DicName, DictionaryValueToString(dictionary));
    }
    public static void SaveDictionary(string DicName, Dictionary<string, float> dictionary)
    {
        SaveDictionary(DicName, DictionaryValueToString(dictionary));
    }

    /// <summary>
    /// 从PlayerPrefs 中读取字典
    /// </summary>
    /// <param name="DicNameout">字典名称</param>
    /// <param name="dictionary">接收数据的字典容器</param>
    public static Dictionary<string, string> LoadDictionary(string DicName)
    {
        string str = Tools.UnicodetoChin(PlayerPrefs.GetString(DicName));

        JsonData json = JsonMapper.ToObject(str);

        Dictionary<string, string> dic = new Dictionary<string, string>();

        if (string.IsNullOrEmpty(str) || json.Keys.Count < 1) return null;

        foreach (var key in json.Keys)
        {
            if (json[key] != null)
            {
                if (dic.ContainsKey(key))
                {
                    dic[key] = json[key].ToString();
                }
                else
                {
                    dic.Add(key, json[key].ToString());
                }

            }
        }

        return dic;
    }

    public static void LoadDictionary(string DicNameout, out Dictionary<string, string> dictionary)
    {
        dictionary = LoadDictionary(DicNameout);
    }
    public static void LoadDictionary(string DicName, out Dictionary<string, int> dictionary)
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();

        foreach (var item in LoadDictionary(DicName))
        {
            dic.Add(item.Key, int.Parse(item.Value));
        }

        dictionary = dic;
    }
    public static void LoadDictionary(string DicName, out Dictionary<string, float> dictionary)
    {
        Dictionary<string, float> dic = new Dictionary<string, float>();

        foreach (var item in LoadDictionary(DicName))
        {
            dic.Add(item.Key, float.Parse(item.Value));
        }

        dictionary = dic;
    }
    public static void LoadDictionary(string DicName, out Dictionary<string, double> dictionary)
    {
        Dictionary<string, double> dic = new Dictionary<string, double>();

        foreach (var item in LoadDictionary(DicName))
        {
            dic.Add(item.Key, double.Parse(item.Value));
        }

        dictionary = dic;
    }

    /// <summary>
    /// Key is string, value to string.
    /// </summary>
    /// <param name="Dic"></param>
    /// <returns></returns>
    public static Dictionary<string, string> DictionaryValueToString(Dictionary<string, int> Dic)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (var item in Dic) dictionary.Add(item.Key, item.Value + "");
        return dictionary;
    }
    public static Dictionary<string, string> DictionaryValueToString(Dictionary<string, float> Dic)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (var item in Dic) dictionary.Add(item.Key, item.Value + "");
        return dictionary;
    }
    public static Dictionary<string, string> DictionaryValueToString(Dictionary<string, double> Dic)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (var item in Dic) dictionary.Add(item.Key, item.Value + "");
        return dictionary;
    }

}
