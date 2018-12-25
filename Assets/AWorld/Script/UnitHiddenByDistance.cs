using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHiddenByDistance : MonoBehaviour {

    public GameObject _Player;
    public DebugLabel _Debug;
    public float _Distant = 200f;

    List<GameObject> _AllGo=new List<GameObject>();


    void Start () {
        _AllGo.AddRange(GetTransformChild(transform));
    }


	void Update () {

        //BUG 没有水

        int i = 0;

        foreach (var go in _AllGo)
        {
            if (Vector3.Distance(go.transform.position,_Player.transform.position)>=_Distant)
            {
                go.SetActive(false);
            }
            else
            {
                go.SetActive(true);
                i++;
            }
        }

        _Debug.AddItem("ShowUnitCount", i);

    }

    List<GameObject> GetTransformChild(Transform transform)
    {
        List<GameObject> _Go=new List<GameObject>();

        if (transform.gameObject.GetComponent<MeshRenderer>()!=null)
        {
            _Go.Add(transform.gameObject);
        }

        foreach (Transform child in transform)
        {
            _Go.AddRange(GetTransformChild(child));
        }

        return _Go;
    }


}
