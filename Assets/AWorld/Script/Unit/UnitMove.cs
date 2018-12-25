using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CharacterController))]
public class UnitMove : MonoBehaviour
{
    public DebugLabel _DebugLabel;

    public float MoveSpeed = 5f;

    [Tooltip("是否受到重力")] public bool OnGravity = true;

    [Tooltip("重力")] public float Gravity = 20.0f;

    public float RotaSpeed = 5f;

    [Tooltip("跳跃力度")] public float JumpForce = 50f;

    [Tooltip("跳跃时移动速度的倍数")] public float InJumpSpeedK = 1.5f;

    public GameObject UnitBody;

    public CharacterController _CharacterController;

    private void Start()
    {
        if (OnGravity)
        {
            StartCoroutine(Graviting());
        }

        InitMove();
    }

    protected virtual void InitMove()
    {

    }

    /// <summary>
    /// 移动协程
    /// </summary>
    IEnumerator MoveEnumerator;
    public void MoveToPostion(Vector3 targerPos, Vector3 xyz)
    {
        LockMovePostion = targerPos;
        LockMoveXyz = xyz;

        if (MoveEnumerator == null)
        {
            MoveEnumerator = Moveing(null, null, 0, 0);
            StartCoroutine(MoveEnumerator);
        }
    }

    public void MoveToGameObject(GameObject gameObject, Vector3 xyz, bool Lock)
    {
        LockMove = Lock;
        MoveToPostion(gameObject.transform.position, xyz);

        MoveLockGameobject = gameObject;

        if (MoveLockGameObjectIEnumerator == null && Lock)
        {
            MoveLockGameObjectIEnumerator = MoveLockGameObject();
            StartCoroutine(MoveLockGameObjectIEnumerator);
        }

    }

    public void StopMove()
    {
        if (MoveEnumerator != null)
        {
            StopCoroutine(MoveEnumerator);
            StopCoroutine(MoveLockGameObjectIEnumerator);
        }

        MoveEnumerator = null;
        MoveLockGameObjectIEnumerator = null;
        LockMove = false;
        IsMoveing = false;
    }

    IEnumerator MoveLockGameObjectIEnumerator;
    GameObject MoveLockGameobject;
    IEnumerator MoveLockGameObject()
    {
        while (true)
        {
            if (LockMovePostion != null)
            {
                Debug.Log(LockMovePostion);
                LockMovePostion = MoveLockGameobject.transform.position;
                yield return 0;
            }
            else
            {
                LockMove = false;
                yield return null;
            }
        }
    }

    Vector3 LockMovePostion;
    Vector3 LockMoveXyz;
    bool LockMove = false;
    public bool IsMoveing { get; private set; }

    IEnumerator Moveing(Animator _Animator, string StateName, int RunningState, int OvetState)
    {
        Vector3 thisPos = Vector3.zero;
        Vector3 vector = Vector3.zero;

        IsMoveing = true;

        if (_Animator != null)
        {
            _Animator.SetInteger(StateName, RunningState);
        }

        do
        {
            thisPos = GetThisPosIgnoreXYZ(LockMovePostion, LockMoveXyz);

            vector = LockMovePostion - thisPos;

            vector = Move(vector, MoveSpeed);

            _CharacterController.Move(vector);

            UnitBody.transform.rotation = GetTargetRot(LockMovePostion, Vector3.up, RotationOffset);

            yield return 0;

        } while (Vector3.Distance(LockMovePostion, thisPos) > 0.2f || LockMove);

        //若目的地是无法到达的位置 协程不会停止
        //transform.position = LockMovePostion;
        if (_Animator != null)
        {
            _Animator.SetInteger(StateName, OvetState);
        }

        _CharacterController.Move(Vector3.zero);

        MoveEnumerator = null;

        IsMoveing = false;

        yield return null;
    }

    Vector3 GetThisPosIgnoreXYZ(Vector3 TargetPost, Vector3 xyz)
    {
        Vector3 thisPos = transform.position;

        if (xyz.x == 0)
        {
            thisPos.x = TargetPost.x;
        }
        if (xyz.y == 0)
        {
            thisPos.y = TargetPost.y;
        }
        if (xyz.z == 0)
        {
            thisPos.z = TargetPost.z;
        }

        return thisPos;
    }


    protected virtual Vector3 Move(Vector3 MoveVector, float MoveSpeed)
    {
        MoveVector.Normalize();

        return MoveVector * MoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 转向协程
    /// </summary>
    IEnumerator TurningIE;
    public void TurnToPostion(Vector3 targerPos, Vector3 xyz)
    {
        if (TurningIE != null)
        {
            StopCoroutine(TurningIE);
        }
        LockLookTargerPos = targerPos;
        LockLookXyz = xyz;
        TurningIE = Turning();
        StartCoroutine(TurningIE);
    }

    public void LookAtGameObject(GameObject gameObject, Vector3 xyz, bool Lock)
    {
        LockLookGameobject = gameObject;
        LockLookXyz = xyz;
        LockLook = Lock;
        TurnToPostion(gameObject.transform.position, xyz);
        if (LockLookGameObjectIE == null && Lock)
        {
            LockLookGameObjectIE = LockLookGameObject();
            StartCoroutine(LockLookGameObjectIE);
        }
    }

    public void StopLookAtGameObject()
    {
        if (LockLookGameObjectIE != null)
        {
            StopCoroutine(LockLookGameObjectIE);
        }

        LockLookGameObjectIE = null;
        LockLook = false;
        IsTurning = false;
    }

    GameObject LockLookGameobject;
    IEnumerator LockLookGameObjectIE;
    IEnumerator LockLookGameObject()
    {
        while (true)
        {
            if (LockLookGameobject != null)
            {
                LockLookTargerPos = LockLookGameobject.transform.position;
                yield return 0;
            }
            else
            {
                LockLook = false;
                yield return null;
            }
        }
    }

    Vector3 LockLookTargerPos;
    Vector3 LockLookXyz;
    bool LockLook = false;
    public Vector3 RotationOffset = Vector3.zero;
    public bool IsTurning { get; private set; }

    IEnumerator Turning()
    {
        Quaternion body;
        Quaternion tar;
        IsTurning = true;
        do
        {
            body = UnitBody.transform.rotation;
            tar = GetTargetRot(LockLookTargerPos, LockLookXyz, RotationOffset);
            UnitBody.transform.rotation = tar;
            //tar = Quaternion.Euler(tar.eulerAngles + RotationOffset);
            yield return 0;
            if (body == tar)
            {
                IsTurning = false;
            }
            else
            {
                IsTurning = true;

            }
        } while (body != tar || LockLook);
        IsTurning = false;
        yield return null;
    }


    /// <summary>
    /// 获得每一帧角度
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="xyz">(0,1,0)仅改变Y轴 (1,1,0)改变X和Y轴</param>
    /// <returns></returns>
    protected Quaternion GetTargetRot(Vector3 targetPos, Vector3 xyz, Vector3 Offset)
    {
        Quaternion thisQua = transform.rotation;
        transform.LookAt(targetPos);
        Quaternion targetRot = Quaternion.Euler(transform.rotation.eulerAngles + Offset);
        transform.rotation = thisQua;

        Vector3 rot = UnitBody.transform.rotation.eulerAngles;

        float speed = RotaSpeed * Time.deltaTime;

        if (xyz.y != 0)
        {
            rot.y = TurnToNextRot(rot.y, targetRot.eulerAngles.y, speed);
        }
        if (xyz.z != 0)
        {
            rot.z = TurnToNextRot(rot.z, targetRot.eulerAngles.z, speed);
        }
        if (xyz.x != 0)
        {
            rot.x = TurnToNextRot(rot.x, targetRot.eulerAngles.x, speed);
        }

        return Quaternion.Euler(rot);
    }

    /// <summary>
    /// Turn
    /// </summary>
    /// <param name="y">当前角度 参照Y轴</param>
    /// <param name="rot">目标角度 参照Y轴</param>
    /// <param name="quatS">旋转速度</param>
    /// <returns>旋转一帧后角度</returns>
    protected float TurnToNextRot(float y, float rot, float quatS)
    {
        rot = AotoChangeTargetAngles(rot, y);

        float nextRot = rot;

        int lr = Math.Sign(Math.Round(rot, 2) - Math.Round(y, 2));

        if (rot == 0f)
        {
            if ((y < 360f - quatS && y > rot + quatS))
            {
                nextRot = y + lr * quatS;
            }
        }
        else
        {
            if (Math.Abs(rot - y) > quatS)
            {
                nextRot = y + lr * quatS;
            }
        }
        return nextRot;
    }

    /// <summary>
    /// 根据当前角度自动转换目标角度
    /// </summary>
    /// <param name="rot">目标角度</param>
    /// <param name="y">当前角度</param>
    /// <returns></returns>
    protected float AotoChangeTargetAngles(float rot, float y)
    {
        rot = rot < 0 ? rot + 360f : rot;

        if (rot <= 180f)
        {
            rot = y > rot + 180f ? rot + 360f : rot;
        }
        else
        {
            rot = y < rot - 180f ? rot - 360f : rot;
        }

        return rot;
    }



    /// <summary>
    /// 跳跃动量
    /// </summary>
    protected float _jumpVector;

    /// <summary>
    /// 是否可以跳跃
    /// </summary>
    protected bool _ReadyJump = false;

    /// <summary>
    /// 无时无刻受到重力
    /// </summary>
    /// <returns></returns>
    IEnumerator Graviting()
    {
        Vector3 vector = new Vector3(0f, Gravity * Time.deltaTime, 0f);

        while (true)
        {

            if (_ReadyJump)
            {
                vector.y = JumpForce;

                _ReadyJump = false;
            }

            if (!IsGrounded())
            {
                vector.y = _jumpVector - Gravity * Time.deltaTime * 10;
            }

            _jumpVector = vector.y;

            _CharacterController.Move(vector * Time.deltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    /// <summary>
    /// 跳跃开关
    /// </summary>
    public virtual void UnitJump()
    {
        if (IsGrounded())
        {
            _ReadyJump = true;
        }
    }


    //Bug-落地平移
    protected bool IsGrounded()
    {
        // return Physics.Raycast(transform.position, Vector3.up, -0.5f);
        return _CharacterController.isGrounded;
    }
}
