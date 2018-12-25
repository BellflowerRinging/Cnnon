using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

public class AttritubeAccessJson : IAttritubeAccessReader
{
    //string JsonFileName = Application.dataPath + "/DataBase/Attritube.json";

    Dictionary<string, Dictionary<string, object>> AttrRowDic;


    public AttritubeAccessJson()
    {
        //StreamReader reader = new StreamReader(JsonName, false);

        //string data = reader.ReadToEnd();

        string data = Resources.Load("DataBase/Attritube").ToString();

        AttrRowDic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(data);

        //JsonConvert.SerializeObject();

        foreach (var item in AttrRowDic)
        {
            item.Value["_TabelHeard"] = ((JArray)item.Value["_TabelHeard"]).ToObject<string[]>();
        }

    }

    public Dictionary<string, object> GetAttrDic(string name)
    {
        if (!AttrRowDic.ContainsKey(name))
        {
            throw new Exception(string.Format("!AttrRowDic.ContainsKey({0})", name));
        }
        else
        {
            return AttrRowDic[name];
        }
    }
}