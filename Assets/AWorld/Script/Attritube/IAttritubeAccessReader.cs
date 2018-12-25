using UnityEngine;
using System.Collections.Generic;

public interface IAttritubeAccessReader 
{
   Dictionary<string, object> GetAttrDic(string name);
}