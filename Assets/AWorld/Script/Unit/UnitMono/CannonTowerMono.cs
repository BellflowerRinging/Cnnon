using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CannonTowerMono : UnitMonoBehaciour
{
    public GameObject Fire;

    public GameObject FireMouse;

    public CannonGameContorl GameContorl;

    [SerializeField] public Tower_Actions _Actions;

    public PlayerStateBarContorl _PlayerStateBarContorl;

    //public float AttackIdel = 2f;

    public override void InitUnitMonoBehaciour()
    {
        base.InitUnitMonoBehaciour();

        PlayerInit();

        //AttackIdel = Attritube.GetFloat(UnitAttritubeType.AttackSpeed);
    }

    public void PlayerInit()
    {
        _PlayerStateBarContorl.SetHpBarLength(1);
        _PlayerStateBarContorl.SetMpBarLength(0);
        _PlayerStateBarContorl.SetLevelText(0);

        Attritube = base.LoadAttritube("CannonTower", typeof(UnitStaticAttritubeType));

        Attritube.SetAttr(UnitDynamicAttritubeType.Mp,0);

        foreach (var item in BuffList)
        {
            Destroy(item);
        }

        BuffList = new List<CannonBUFF>();
    }

    public override void Damage(float value)
    {
        base.Damage(value);

        float Hp = Attritube.GetFloat(UnitDynamicAttritubeType.Hp);
        float MaxHp = Attritube.GetFloat(UnitStaticAttritubeType.MaxHp);

        _PlayerStateBarContorl.SetHpBarLength(Hp / MaxHp);
    }

    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("CannonTower", type);
    }

    public override void Attack(int type)
    {
        _Actions.Fire();
    }

    /*public override void LookAtGameObject(GameObject gameObject, Vector3 xyz, bool Lock)
    {
        base.LookAtGameObject(gameObject, xyz, Lock);
    }*/

    public override void Attack(int type, GameObject gameObject)
    {
        //TurnToPostion(gameObject.transform.position, Vector3.up);
        LookAtGameObject(gameObject, Vector3.up, true);

        AttackingIE = Attacking(gameObject);
        StartCoroutine(AttackingIE);

        base.Attack(type, gameObject);
    }

    IEnumerator AttackingIE;
    public void SelectAttackGameObject()
    {
        foreach (var item in GameContorl.DcrList)
        {
            if (item != null)
            {
                if (!OnAttack)
                {
                    Attack(0, item);
                }
                break;
            }
        }
    }

    public IEnumerator Attacking(GameObject go)
    {
        OnAttack = true;

        while (IsTurning()) { yield return new WaitForSeconds(Time.deltaTime); }

        while (go != null)
        {
            _Actions.Fire();

            UnitMonoBehaciour targetMono = go.GetComponent<UnitMonoBehaciour>();

            DoBuff(targetMono);

            float Damage = Attritube.GetFloat(UnitStaticAttritubeType.Attack) + Attritube.GetFloat(UnitDynamicAttritubeType.AttackAdd);

            BoomParcitle(go, Damage);

            targetMono.Damage(Damage);

            ParticleContorl.Play(ParticleType.fire, FireMouse, Fire.transform);

            //if (AttackIdel >= 1f)
            //{
            _Actions.Idle();
            //}

            if (go == null) break;

            float idel = Attritube.GetFloat(UnitStaticAttritubeType.AttackSpeed) + Attritube.GetFloat(UnitDynamicAttritubeType.AttackSpeedAdd);

            yield return new WaitForSeconds(idel);
        }

        _Actions.Idle();

        OnAttack = false;

        SelectAttackGameObject();
    }

    public override void StopAttack()
    {
        base.StopAttack();

        if (AttackingIE != null)
        {
            StopCoroutine(AttackingIE);
        }

        OnAttack = false;
    }

    List<CannonBUFF> BuffList = new List<CannonBUFF>();

    public List<CannonBUFF> GetBuffList() { return BuffList; }

    public void InitBuff(CannonBUFF buff)
    {
        gameObject.AddComponent(buff.GetType());

        BuffList.Add((CannonBUFF)gameObject.GetComponent(buff.GetType()));
    }

    void DoBuff(UnitMonoBehaciour targetMono)
    {
        foreach (var buff in BuffList)
        {
            buff.DoBUFF(this, targetMono);
        }
    }


    public void BoomParcitle(GameObject parent, float Damage)
    {
        float Scale = Damage / 100;
        Vector3 vect = new Vector3(1, 1, 1) * Mathf.Clamp(Scale, 3f, 15f);
        ParticleContorl.Play(ParticleType.boom, parent.transform.position, parent.transform.rotation, vect);
    }


    public void MpAdd(float value)
    {
        float mp = Attritube.GetFloat(UnitDynamicAttritubeType.Mp);
        float maxMP = Attritube.GetFloat(UnitStaticAttritubeType.MaxMp);

        mp += value;

        if (mp >= maxMP)
        {
            PlayerLevelUp();
        }
        else
        {
            _PlayerStateBarContorl.SetMpBarLength(mp / maxMP);
            Attritube.SetAttr(UnitDynamicAttritubeType.Mp, mp);
            Attritube.SetAttr(UnitStaticAttritubeType.MaxMp, maxMP);
        }
    }

    public void PlayerLevelUp()
    {

        float maxMP = Attritube.GetFloat(UnitStaticAttritubeType.MaxMp);

        maxMP += GameContorl.LevelMpAdd;

        int level = Attritube.GetIntger(UnitStaticAttritubeType.Level);

        level++;

        _PlayerStateBarContorl.SetLevelText(level);

        _PlayerStateBarContorl.SetMpBarLength(0);
        Attritube.SetAttr(UnitDynamicAttritubeType.Mp, 0);
        Attritube.SetAttr(UnitStaticAttritubeType.MaxMp, maxMP);
        Attritube.SetAttr(UnitStaticAttritubeType.Level, level);

        GameContorl.RandomBuff();
    }


    public override void Death()
    {
        GameContorl.GameOver();
    }
}
