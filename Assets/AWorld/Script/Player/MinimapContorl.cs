using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapContorl : MonoBehaviour
{

    public GameObject _Player;
    public GameObject _PlayerCamera;

    float _Pos_Y;
    // Use this for initialization
    void Start()
    {
        _Pos_Y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_Player != null)
        {
            Vector3 vector = _Player.transform.position;
            vector.y = _Pos_Y;
            transform.position = vector;
            transform.rotation = Quaternion.Euler(90f, _PlayerCamera.transform.rotation.eulerAngles.y, 0f);
        }
    }
}
