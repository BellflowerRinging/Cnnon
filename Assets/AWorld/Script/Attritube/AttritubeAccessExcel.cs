using UnityEngine;
using System.Collections;
using Excel;
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Data;
using System.Collections.Generic;
using LitJson;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;



public class AttritubeAccessExcel : IAttritubeAccessReader
{

    Dictionary<string, Dictionary<string, object>> AttrRowDic = new Dictionary<string, Dictionary<string, object>>();

    string JsonFileName;

    public AttritubeAccessExcel(string ExcelName,string JsonName)
    {
        JsonFileName = JsonName;

        FileStream stream = File.Open(ExcelName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();

        foreach (DataTable table in result.Tables)
        {
            int count = table.Columns.Count;
            string[] ColumnNames = null;

            if (count ==0 || count ==1)
            {
                throw new Exception("table.Columns.Count="+count);
            }

            foreach (DataRow row in table.Rows)
            {
                if (string.IsNullOrEmpty(row[0].ToString())) continue;

                if (row[0].ToString().StartsWith("#"))
                {
                    ColumnNames = getColumnNames(count,row);
                }
                else if (!row[0].ToString().StartsWith("//"))
                {
                    ThrowException(ColumnNames,count);

                    Dictionary<string, object> AttrDic = new Dictionary<string, object>();

                    for (int i = 0; i < ColumnNames.Length; i++)
                    {
                        AttrDic.Add(ColumnNames[i],row[i]);
                    }

                    AttrDic.Add("_TabelHeard",ColumnNames);

                    WriteToAttrRowDic(row[0].ToString(),AttrDic);
                }


            }
        }

        stream.Close();

        /*
        foreach (var item in AttrRowDic)
        {
            foreach (var it in item.Value)
            {
                Debug.Log(it.Key+"="+it.Value);
            }
        }*/

        WriteToJsonFile(JsonConvert.SerializeObject(AttrRowDic));
    }

    public void WriteToJsonFile(string jsonString)
    {
        StreamWriter writer = new StreamWriter(JsonFileName,false);

        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        var ss = reg.Replace(jsonString, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });

        writer.WriteLine(ss);

        writer.Close();
    }

    //同一表格不同类型的属性名字重复会出现BUG
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

    string[] getColumnNames(int count, DataRow row)
    {
        string[] ColumnNames = new string[count];

        string[] retCn;

        ColumnNames[0] = row[0].ToString().Replace("#", "");

        for (int i = 1; i < count; i++)
        {
            string str = row[i].ToString();

            if (string.IsNullOrEmpty(str))
            {
                retCn = new string[i];

                for (int j = 0; j < i; j++)
                {
                    retCn[j] = ColumnNames[j];
                }
                return retCn;
            }

            ColumnNames[i] = row[i].ToString();
        }

        return ColumnNames;
    }

    void ThrowException(string[] ColumnNames,int ColumnCount)
    {
        if (ColumnNames == null)
        {
            throw new Exception("ColumnNames==null");
        }
    }

    void WriteToAttrRowDic(string name,Dictionary<string, object> AttrDic)
    {
        if (AttrRowDic.ContainsKey(name))
        {
            AttrRowDic[name] = AttrDic;

            Debug.Log("已有属性被覆盖：" + name);
        }
        else
        {
            AttrRowDic.Add(name, AttrDic);
        }
    }

}
