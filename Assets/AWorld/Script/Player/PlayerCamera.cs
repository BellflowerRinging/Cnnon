using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Bround
{
    public float top;
    public float buttom;
}

public class PlayerCamera : MonoBehaviour
{
    public DebugLabel _DebugLabel;

    public GameObject _PlayerGo;
    public Vector3 _LookAtOffset;
    public float _Speed = 20f;
    public float _Distance = 20f;
    public float _MouseScrollWheelSpeed = 10f;
    public bool _Reverse_X = true;
    public bool _Reverse_Y = false;
    public BallContorl _BallContorl;
    public BallContorl _DisdanceContorl;

    public Bround _EulerBround;
    public Bround _DistanceBround;

    // Use this for initalization
    void Start()
    {
        InitCamera();

        _EulerBround.top = Clamp360(_EulerBround.top);
        _EulerBround.buttom = Clamp360(_EulerBround.buttom);

        if (_EulerBround.top < _EulerBround.buttom)
        {
            _EulerBround.top = _EulerBround.buttom;
        }
    }

    public void InitCamera()
    {
        _PlayerPos = _PlayerGo.transform.position + _LookAtOffset;

        transform.LookAt(_PlayerPos);

        _Distance = Mathf.Clamp(_Distance, _DistanceBround.top, _DistanceBround.buttom);

        _PlayerPos.z = _PlayerPos.z - _Distance;

        transform.position = _PlayerPos;

        //_Rot = Vector3.zero;
    }

    Vector3 _PlayerPos;
    Vector3 Vector;

    [SerializeField] Vector3 _Rot;

    [System.Runtime.InteropServices.DllImport("user32.dll")] //引入dll
    public static extern int SetCursorPos(int x, int y);

    private void LateUpdate()
    {
        
        //SetCursorPos(Screen.width/2,Screen.height/2);

        Rotation();

        Disdance();

        IfHidePlayer();
    }

    void Rotation()
    {
        float inputX=0;
        float inputY=0;

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //InputByWindow(ref _Rot);
            inputX = Input.GetAxis("Mouse X");
            inputY = Input.GetAxis("Mouse Y");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //InputByAndroid(ref _Rot);
            Vector2 vector = _BallContorl.GetVectorOffset();
            inputX = vector.x;
            inputY = vector.y;
        }

        if (_Reverse_Y) inputX = -inputX;
        if (_Reverse_X) inputY = -inputY;

        _Rot.y += inputX * Time.deltaTime * _Speed;
        _Rot.x += inputY * Time.deltaTime * _Speed;

        //InputByAndroid(ref _Rot);

        _DebugLabel.AddItem("Camera_Rot", _Rot);

        _Rot.y = Clamp360(_Rot.y);
        _Rot.x = Clamp360(_Rot.x);

        _Rot.x = Mathf.Clamp(_Rot.x, _EulerBround.buttom, _EulerBround.top);

        transform.rotation = Quaternion.Euler(_Rot);

        _PlayerPos = _PlayerGo.transform.position + _LookAtOffset;

        transform.position = _PlayerPos + transform.rotation * new Vector3(0, 0, -_Distance);
    }

    void Disdance()
    {
        float inputScrollWheel=0;
        float _WheelSpeed=0;

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            inputScrollWheel = -Input.GetAxis("Mouse ScrollWheel");
            _WheelSpeed = _MouseScrollWheelSpeed;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //InputByAndroid(ref _Rot);
            _WheelSpeed = _MouseScrollWheelSpeed / 20f;
            inputScrollWheel = -_DisdanceContorl.GetVectorOffset().y;
        }



        if (inputScrollWheel != 0)
        {
            _Distance += inputScrollWheel * _WheelSpeed;

            _Distance = Mathf.Clamp(_Distance, _DistanceBround.top, _DistanceBround.buttom);
        }
    }

    void IfHidePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(_PlayerPos, transform.position - _PlayerPos, out hit))
        {
            _DebugLabel.AddItem("HidePlayerHit", hit.collider.gameObject.name);
            //---------------------------------------待优化！！
            if (hit.collider.gameObject.name != gameObject.name 
                && hit.collider.gameObject.name!= _PlayerGo.name
                && hit.collider.gameObject.GetComponent<UnitMonoBehaciour>()==null 
                && hit.collider.gameObject.name != "Weapon")
            {
                Vector3 vector = hit.point;
                if (hit.collider.gameObject.name == "Terrain")
                {
                    vector.y += 4f;
                }
                transform.position = vector;
            }
        }
    }


    float Clamp360(float value)
    {
        if (value < -360) return 720 + value;
        if (value > 360) return value - 720;
        return value;
    }
}