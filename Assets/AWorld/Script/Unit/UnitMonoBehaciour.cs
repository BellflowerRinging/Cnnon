using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnitMove))]
public class UnitMonoBehaciour : MonoBehaviour, IUnit
{

    public GameObject _Model;

    public GameObject _Body;

    protected CharacterController _CharacterController;

    protected UnitMove _UnitMove;

    protected GameObject _PlayerCamera;


    //待收集---------------
    private string _DamageTextC_Prefab_Path = "DamageTextC";
    private string _UnitHpBarC_Prefab_Path = "UnitHpBarC";


    //------
    public IAttritube Attritube { get; protected set; }


    public Type type;
    //*--
    [HideInInspector] public Renderer _Renderer;
    protected HpBarScript _HpBar;

    [SerializeField] protected HpBarSetting hpBarSetting;
    [SerializeField] protected DamageTextSetting damageTextSetting;


    void Start()
    {
        Attritube = LoadAttritube("DefalutUnit", typeof(UnitStaticAttritubeType));

        InitUnitMonoBehaciour();

        OnDamage = false;

        OnAttack = false;
    }

    public virtual void InitUnitMonoBehaciour()
    {
        _PlayerCamera = GameObject.Find("PlayerCamera");

        _Renderer = _Model.GetComponent<MeshRenderer>();

        if (_Renderer == null) _Renderer = _Model.GetComponent<SkinnedMeshRenderer>();

        if (hpBarSetting.Use)
        {
            _HpBar = _InitHpBar();
        }

        _CharacterController = GetComponent<CharacterController>();

        _UnitMove = GetComponent<UnitMove>();

        _DefCol = _Renderer.material.color;

    }

    protected virtual IAttritube LoadAttritube(string AttrName, Type type)
    {
        UnitAttritube attribute = new UnitAttritube(AttritubeAccess.GetAttrDic(AttrName), type);

        attribute.SetAttr(UnitDynamicAttritubeType.Hp, attribute.GetAttr(UnitStaticAttritubeType.MaxHp));

        attribute.SetAttr(UnitDynamicAttritubeType.Mp, attribute.GetAttr(UnitStaticAttritubeType.MaxMp));

        return attribute;
    }

    public bool OnDamage { get; protected set; }

    public bool OnAttack { get; protected set; }

    public Color _DefCol;

    /// <summary>
    /// 受到伤害处理
    /// </summary>
    /// <param name="value"></param>
    public virtual void Damage(float value)
    {
        OnDamage = true;

        if (this == null) return;

        NewDamageText(value);

        float Hp = Attritube.GetFloat(UnitDynamicAttritubeType.Hp);
        float MaxHp = Attritube.GetFloat(UnitStaticAttritubeType.MaxHp);

        if ((Hp -= value) <= 0) Death();

        Attritube.SetAttr(UnitDynamicAttritubeType.Hp, Hp);

        FlashHpBat(Hp, MaxHp);

        if (value < 0)
        {
            StartCoroutine(DamageColor(Color.green));
        }
        else
        {
            StartCoroutine(DamageColor(Color.red));
        }

    }

    protected virtual void FlashHpBat(float Hp, float MaxHp)
    {
        if (_HpBar != null)
        {
            _HpBar.SetHpBarLength(Hp / MaxHp);
        }
    }

    protected virtual GameObject NewDamageText(float value)
    {
        if (damageTextSetting.Use)
        {
            return NewDamageText(value, damageTextSetting.Offset, damageTextSetting.disdent, damageTextSetting.SizeMult, damageTextSetting.MinSizeMult, damageTextSetting.MaxSizeMult);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 创建一个伤害文本
    /// </summary>
    /// <param name="value">伤害量</param>
    /// <param name="disdent">离父距离</param>
    /// <param name="SizeMult">大小倍数，伤害量是MaxHP的100%则文本大小为SizeMult倍</param>
    /// <param name="MinSize">最大size</param>
    /// <param name="MaxSize">最小size</param>
    /// <returns>创建的文本</returns>
    GameObject NewDamageText(float value, Vector3 Offset, float disdent, float SizeMult, float MinSize, float MaxSize)
    {
        if (_PlayerCamera == null)
        {
            _PlayerCamera = GameObject.Find("PlayerCamera");
        }

        GameObject textC = (GameObject)Instantiate(Resources.Load(_DamageTextC_Prefab_Path));
        textC.transform.position = transform.position + Vector3.Normalize(_PlayerCamera.transform.position - transform.position) * disdent + Offset;
        textC.GetComponentInChildren<Text>().text = "" + (int)value;
        textC.transform.localScale *= Mathf.Clamp(SizeMult * (value / Attritube.GetFloat(UnitStaticAttritubeType.MaxHp)), MinSize, MaxSize);
        return textC;
    }

    IEnumerator DamageRedIE;
    /// <summary>
    /// 受到伤害后恢复颜色
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageColor(Color color)
    {
        _Renderer.material.color = color;
        yield return new WaitForSeconds(0.2f);
        _Renderer.material.color = _DefCol;
        OnDamage = false;
    }


    /// <summary>
    /// 初始化血条
    /// </summary>
    /// <param name="Offset"></param>
    /// <param name="ScaleMult">缩放倍数</param>
    /// <returns></returns>
    public virtual HpBarScript _InitHpBar()
    {
        GameObject bar = (GameObject)Instantiate(Resources.Load(_UnitHpBarC_Prefab_Path), transform);
        bar.transform.position = bar.transform.position + hpBarSetting.Offset * 10;
        bar.transform.localScale *= hpBarSetting.ScaleMult;
        return bar.GetComponent<HpBarScript>();
    }


    /// <summary>
    /// 死亡处理
    /// </summary>
    public virtual void Death()
    {
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// 攻击动作
    /// </summary>
    public virtual void Attack(int type) { }

    public virtual void Attack(int type, GameObject gameObject) { }


    public virtual Quaternion GetRotation()
    {
        return _Body.transform.rotation;
    }

    public virtual void TurnToPostion(Vector3 vector, Vector3 xyz)
    {
        _UnitMove.TurnToPostion(vector, xyz);
    }

    public virtual void MoveToPostion(Vector3 vector3, Vector3 xyz)
    {
        _UnitMove.MoveToPostion(vector3, xyz);
    }

    public virtual void MoveToGameObject(GameObject gameObject, Vector3 xyz, bool Lock)
    {
        _UnitMove.MoveToGameObject(gameObject, xyz, Lock);
    }

    public virtual void LookAtGameObject(GameObject gameObject, Vector3 xyz, bool Lock)
    {
        _UnitMove.LookAtGameObject(gameObject, xyz, Lock);
    }

    public virtual bool IsMoveing() { return _UnitMove.IsMoveing; }

    public virtual bool IsTurning() { return _UnitMove.IsTurning; }



    public virtual void Jump()
    {
        _UnitMove.UnitJump();
    }

    public virtual void StopAttack() { }

    public virtual void StopMove()
    {
        _UnitMove.StopMove();
    }

    public virtual void StopLook()
    {
        _UnitMove.StopLookAtGameObject();
    }


}

[Serializable]
public class HpBarSetting
{
    public bool Use = true;
    public Vector3 Offset;
    public float ScaleMult = 1f;
}

[Serializable]
public class DamageTextSetting
{
    public bool Use = true;
    public Vector3 Offset;
    [Tooltip("离父距离")] public float disdent = 5f;
    [Tooltip("最大size")] public float MinSizeMult = 1f;
    [Tooltip("最小size")] public float MaxSizeMult = 2f;
    [Tooltip("大小倍数，伤害量是MaxHP的100%则文本大小为SizeMult倍")] public float SizeMult = 2f;
}