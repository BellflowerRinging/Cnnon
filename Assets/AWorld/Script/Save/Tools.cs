using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class Tools
{
    public static string UnicodetoChin(string text)
    {
        MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\\\w]{4})");
        if (mc.Count == 0)
        {
            return text;
        }

        for (int i = 0; i < mc.Count; i++)
        {
            string c = mc[i].Value;
            text.Replace(c, ((char)Convert.ToInt32(c.Replace("\\\\u", ""), 16))+"");
        }

        return text;
    }

    public static Dictionary<String, String> DictionarySS(Dictionary<String,float> dic) {
        Dictionary<string, string> rdic = new Dictionary<string, string>();
        foreach (var item in dic)
        {
            rdic.Add(item.Key, item.Value + "");
        }

        return rdic;
    }

    public static bool CheckDic(Dictionary<string, float> A, Dictionary<string, float> B)
    {
        if (A == null && B == null) return true;

        if (A == null || B == null) return false;

        if (A.Count != B.Count) return false;

        foreach (var dir in A)
        {
            if (!B.ContainsKey(dir.Key)) return false;
            else if (B[dir.Key] != dir.Value) return false;
        }

        return true;
    }

    public static bool CheckDic(Dictionary<string, object> A, Dictionary<string, object> B)
    {
        if (A == null && B == null) return true;

        if (A == null || B == null) return false;

        if (A.Count != B.Count) return false;

        foreach (var dir in A)
        {
            if (!B.ContainsKey(dir.Key)) return false;
            else if (B[dir.Key] != dir.Value) return false;
        }

        return true;
    }
}
