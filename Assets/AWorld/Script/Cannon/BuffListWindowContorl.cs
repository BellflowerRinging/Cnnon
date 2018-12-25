using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffListWindowContorl : WindowContorl
{

    public UISelectBUFF UISelectBUFF;

    public List<CannonBUFF> BuffList;
    List<DefaultBuffPlane> PlaneList = null;

    public GameObject DefaultBuffPlane;

    private void Start()
    {

    }

    public void InitBuffListWindow()
    {
        if (PlaneList == null)
        {
            InitPlaneGameObject();

            WriteToPlane();
        }
    }

    public void InitPlaneGameObject()
    {
        Transform parent = DefaultBuffPlane.transform.parent;

        for (int i = 0; i < BuffList.Count - 1; i++)
        {
            Instantiate<GameObject>(DefaultBuffPlane, parent);
        }

        PlaneList = new List<DefaultBuffPlane>(GetComponentsInChildren<DefaultBuffPlane>());
    }

    public void WriteToPlane()
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            PlaneList[i].Name.text = BuffList[i].ChinName;
            PlaneList[i].Introduce.text = BuffList[i].Introduce;
        }
    }
}
