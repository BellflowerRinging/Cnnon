using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttritubeAccess : MonoBehaviour
{
    public static string ExcelName = "Attritube.xlsx";

    public static string JsonName;

    static IAttritubeAccessReader AttrAccess;


    void Start()
    {
        if (AttrAccess == null) Init();
    }

    private static void Init()
    {
        ExcelName = Application.dataPath + "/AWorld/Excel/" + ExcelName;

        JsonName = Application.dataPath + "/Resources/DataBase/Attritube.json";


        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            AttrAccess = new AttritubeAccessExcel(ExcelName, JsonName);
        }
        else
        {
            AttrAccess = new AttritubeAccessJson();
        }



    }

    public static Dictionary<string, object> GetAttrDic(string name)
    {
        if (AttrAccess == null) Init();

        return AttrAccess.GetAttrDic(name);
    }
}
