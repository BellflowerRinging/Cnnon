using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : UnitMonoBehaciour,IUnit {

    public DebugLabel _DebugLabel;
    
    //[SerializeField] private PlayerMove _PlayerMove;
    [SerializeField] private Animator _Animator;

    [SerializeField] private PlayerStateBarContorl _StateBatContorl;

    [Tooltip("武器的RootBone")] public GameObject _Weapon;

    PlayerControl _PlayerControl;

    public PlayerStateBarContorl StateBatContorl
    {
        get
        {
            return _StateBatContorl;
        }

        private set
        {
            _StateBatContorl = value;
        }
    }

    public override void InitUnitMonoBehaciour()
    {

        _Renderer = _Model.GetComponent<SkinnedMeshRenderer>();

        _PlayerControl = GetComponent<PlayerControl>();

        _PlayerCamera = GameObject.Find("PlayerCamera");

        StateBatContorl.SetHpBarLength(Attritube.GetFloat(PlayerAttritubeType.Hp)/ Attritube.GetFloat(PlayerAttritubeType.MaxHp));

        StateBatContorl.SetMpBarLength(Attritube.GetFloat(PlayerAttritubeType.Mp) / Attritube.GetFloat(PlayerAttritubeType.MaxMp));

        StateBatContorl.SetLevelText(Attritube.GetIntger(PlayerAttritubeType.Level));

        _UnitMove = GetComponent<PlayerMove>();
    }

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("Player", typeof(PlayerAttritubeType));
    }

    //如何体现攻速呢？？
    public override void Attack(int type)
    {
        if (!(type == 1 || type == 0)) return;

        if (_PlayerControl.GetPlayerState() != PlayerState.Jump)
        {
            float DamageValue = Attritube.GetFloat(PlayerAttritubeType.Attack);

            switch (type)
            {
                case 0:
                    _Animator.SetInteger("State", (int)_AnimState.Attack);
                    break;

                case 1:
                    _Animator.SetInteger("State", (int)_AnimState.Attack_2);
                    DamageValue *= 0.75f;
                    break;

                default:
                    break;
            }


            if (!OnAttack)
            {
                StartCoroutine(OnAttackWhile(_Weapon.GetComponent<ColliderMessage>(), DamageValue));
            }
        }
    }

    IEnumerator OnAttackWhile(ColliderMessage message, float DamageValue)
    {
        OnAttack = true;

        List<IUnit> AlreadyAttack = new List<IUnit>();

        do { yield return new WaitForFixedUpdate();} while (_Animator.IsInTransition(0));

        do
        {
            if (_Animator.IsInTransition(0)) break;

            Collider _coll = message.Enter;

            AttackDoDamage(_coll, DamageValue, AlreadyAttack);

            yield return new WaitForFixedUpdate();
        } while (_PlayerControl.GetPlayerState() == PlayerState.Attack);

        AlreadyAttack.Clear();
        OnAttack = false;
    }

    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="_coll">与武器碰撞器发生碰撞的碰撞器</param>
    /// <param name="DamageValue">伤害数值</param>
    /// <param name="AlreadyAttack">记录单次攻击已经造成伤害的对象</param>
    void AttackDoDamage(Collider _coll,float DamageValue,List<IUnit> AlreadyAttack)
    {
        if (_coll != null)
        {
            IUnit unit = _coll.gameObject.GetComponent<UnitMonoBehaciour>();

            if (unit != null && _coll.gameObject.gameObject != gameObject && !unit.OnDamage)
            {
                if (!AlreadyAttack.Contains(unit))
                {
                    AlreadyAttack.Add(unit);
                    unit.Damage(DamageValue);
                }
            }
        }
    }

    protected override void FlashHpBat(float Hp, float MaxHp)
    {
        StateBatContorl.SetHpBarLength(Hp / MaxHp);
    }

    public override Quaternion GetRotation()
    {
        Vector3 rota = _PlayerCamera.transform.rotation.eulerAngles;
        return Quaternion.Euler(0, rota.y, 0f); ;
    }

}
