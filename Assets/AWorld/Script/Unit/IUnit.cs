using UnityEngine;

public interface IUnit
{
    /// <summary>
    /// 属性
    /// </summary>
    IAttritube Attritube{ get; }

    /// <summary>
    /// 是否正受到伤害
    /// </summary>
    bool OnDamage {get;}

    /// <summary>
    /// 受到伤害处理
    /// </summary>
    /// <param name="value"></param>
    void Damage(float value);

    /// <summary>
    /// 死亡处理
    /// </summary>
    void Death();

    /// <summary>
    /// 是否正在攻击
    /// </summary>
    bool OnAttack { get; }

    /// <summary>
    /// 攻击动作
    /// </summary>
    void Attack(int type);

    void Attack(int type, GameObject gameObject);

    /// <summary>
    /// 获得当前朝向
    /// </summary>
    /// <returns></returns>
    Quaternion GetRotation();

    /// <summary>
    /// 跳跃
    /// </summary>
    void Jump();

    /// <summary>
    /// 移动到位置
    /// </summary>
    /// <param name="vector3"></param>
    void MoveToPostion(Vector3 vector3, Vector3 xyz);

    /// <summary>
    /// 移动到Gameobject 
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="xyz">(1,0,1)允许X和Z轴移动</param>
    /// <param name="Lock">是否锁定跟随</param>
    void MoveToGameObject(GameObject gameObject, Vector3 xyz, bool Lock);


    /// <summary>
    /// 转向位置
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="xyz">(1,0,1)允许X和Z轴移动</param>
    void TurnToPostion(Vector3 vector, Vector3 xyz);

    /// <summary>
    /// 转向Gameobject
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="xyz">(1,0,1)允许X和Z轴移动</param>
    /// <param name="Lock">是否锁定跟随</param>
    void LookAtGameObject(GameObject gameObject, Vector3 xyz, bool Lock);

    void StopAttack();

    void StopMove();

    void StopLook();
}