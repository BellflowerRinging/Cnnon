using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : UnitMove
{
    public float RunSpeedK = 2.0f;

    public bool _WalkState = true;

    public bool _AttackStandy = true;

    public GameObject _CameraGo;

    private bool _DirectionLockByCamera = true;

    public Animator _Animator;

    public BallContorl _BallContorl;

    private PlayerControl _PlayerControl;

    void Start()
    {
        _CharacterController = GetComponent<CharacterController>();

        _PlayerControl = GetComponent<PlayerControl>();
    }

    PlayerState _PlayerState;

    void Update()
    {
        Vector2 _InputHV = Vector2.zero;

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            _InputHV = GetInputHV();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            _InputHV = _BallContorl.GetVectorOffset();
            _InputHV.Normalize();
        }

        Vector3 MoveVector = new Vector3(_InputHV.x, 0f, _InputHV.y);

        _PlayerState = _PlayerControl.GetPlayerState();

        if (_PlayerState == PlayerState.Attack)
        {
            MoveVector.x = 0;
            MoveVector.z = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            PlayerJump();
        }

        if (MoveVector.x != 0 || MoveVector.z != 0 || _ReadyJump)
        {

            //move
            if (MoveVector.x != 0 || MoveVector.z != 0)
            {
                float speed = 0;
                if (Input.GetKey(KeyCode.LeftShift) || _WalkState)
                {
                    speed = MoveSpeed;
                    MoveVector = Move(MoveVector, speed, _Animator, "State", (int)_AnimState.Walk);
                }
                else
                {
                    speed = MoveSpeed * RunSpeedK;
                    MoveVector = Move(MoveVector, speed, _Animator, "State", (int)_AnimState.Run00);
                }



                UnitBody.transform.rotation = PlayerTurn(_InputHV.y, _InputHV.x);
            }

            //jump
            if (_ReadyJump)
            {
                GetJumpVector(ref MoveVector, _Animator, "State", (int)_AnimState.Jump);
            }
        }

        else
        {

            if (_AttackStandy)
            {
                _Animator.SetInteger("State", (int)_AnimState.AttackStandy);

            }
            else
            {
                _Animator.SetInteger("State", (int)_AnimState.Idel);
            }
        }


        /**/
        if (!IsGrounded())
        {
            MoveVector.y = _jumpVector - Gravity * Time.deltaTime;
        }

        _jumpVector = MoveVector.y;
        /**/

        if (IsGrounded() && _Animator.GetCurrentAnimatorClipInfoCount(0) > 0 && _Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Jump"))
        {
            MoveVector.x = 0;
            MoveVector.z = 0;
        }


        _DebugLabel.AddItem("Move Vector", MoveVector);



        _CharacterController.Move(MoveVector * Time.deltaTime);


    }

    /// <summary>
    /// Vector2.x = Horizontal;
    /// Vector2.y = Vertical;
    /// </summary>
    /// <returns></returns>
    Vector2 GetInputHV()
    {
        Vector2 v = Vector2.zero;

        if (Input.GetKey("w"))
        {
            v.y = 1;
        }

        else if (Input.GetKey("s"))
        {
            v.y = -1;
        }

        if (Input.GetKey("a"))
        {
            v.x = -1;
        }

        else if (Input.GetKey("d"))
        {
            v.x = 1;
        }

        return v;
    }

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="MoveVector">移动向量</param>
    /// <param name="MoveSpeed">步行速度</param>
    protected Vector3 Move(Vector3 MoveVector, float MoveSpeed, Animator _Animator, string StateName, int State)
    {
        _Animator.SetInteger(StateName, State);

        if (_DirectionLockByCamera)
        {
            Vector3 v = _CameraGo.transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(0, v.y, 0);

            MoveVector = Quaternion.Euler(0f, v.y, 0f) * MoveVector;
        }

        MoveVector.Normalize();

        return MoveVector * MoveSpeed;
    }


    /// <summary>
    /// 转身
    /// </summary>
    /// <param name="Vertical">水平动量</param>
    /// <param name="Horizontal">垂直动量</param>
    Quaternion PlayerTurn(float Horizontal, float Vertical)
    {
        if (_Animator.GetCurrentAnimatorClipInfoCount(0) > 0 && _Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idel"))
        {
            _Animator.SetInteger("State", (int)_AnimState.Walk);
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            return PlayerTurnInPc(Horizontal, Vertical);
        }
        else
        {
            return PlayerTurnInBallContorl(Horizontal, Vertical);
        }
    }

    Quaternion PlayerTurnInPc(float Horizontal, float Vertical)
    {
        Vector3 quat = UnitBody.transform.rotation.eulerAngles;

        float quatS = RotaSpeed * Time.deltaTime; //speed

        float rot = GetTargetAngles(Horizontal, Vertical);

        return Quaternion.Euler(0f, TurnToNextRot(quat.y, rot, quatS), 0f);
    }

    Quaternion PlayerTurnInBallContorl(float Vertical, float Horizontal)
    {
        Vector3 v = new Vector3(Horizontal, 0, Vertical);
        return Quaternion.Euler(transform.rotation.eulerAngles + Quaternion.LookRotation(v).eulerAngles);
    }

    /// <summary>
    /// 根据输入方向 计算八个方向
    /// </summary>
    /// <param name="Vertical">Input Vertical</param>
    /// <param name="Horizontal">Input Horizontal</param>
    /// <returns></returns>
    float GetTargetAngles(float Vertical, float Horizontal)
    {
        float rot = 0;

        if (Vertical > 0)
        {
            rot = 45f * Math.Sign(Horizontal);
        }
        else if (Vertical < 0)
        {
            rot = 180f - 45f * Math.Sign(Horizontal);
        }
        else
        {
            rot = 180f - 90f * Math.Sign(Horizontal);
        }

        return rot + transform.rotation.eulerAngles.y;
    }

    void GetJumpVector(ref Vector3 MoveVector, Animator _Animator, string StateName, int State)
    {
        _ReadyJump = false;

        if (_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Jump"))
        {
            if (IsGrounded())
            {
                MoveVector.x = 0;
                MoveVector.z = 0;
            }

            return;
        }

        _Animator.SetInteger(StateName, State);

        MoveVector *= InJumpSpeedK;

        if (IsGrounded())
        {
            MoveVector.y = JumpForce;
        }

        return;
    }

    public void PlayerJump()
    {
        if (_PlayerState != PlayerState.Attack)
        {
            _ReadyJump = true;
        }
    }
}


