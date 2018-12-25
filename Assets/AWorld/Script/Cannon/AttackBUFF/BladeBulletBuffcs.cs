using UnityEngine;
using System.Collections;
using System;

public class BladeBulletBuff : CannonBUFF
{
    protected override IAttritube LoadAttritube(string AttrName, Type type)
    {
        return base.LoadAttritube("BladeBullet", type);
    }

    public override void DoBUFF(UnitMonoBehaciour unit, UnitMonoBehaciour target)
    {
        float rand = UnityEngine.Random.Range(0, 1f);

        if (rand <= Value_0)
        {
            float maxhp = target.Attritube.GetFloat(UnitStaticAttritubeType.MaxHp);

            StartCoroutine(WaitAUpdata(target, 0.3f, maxhp * Value_1));
        }
    }

    IEnumerator WaitAUpdata(UnitMonoBehaciour unit, float time, float damage)
    {
        yield return new WaitForSeconds(time);

        if (unit != null)
        {
            ParticleContorl.Play(ParticleType.Blade, unit.transform);

            unit.Damage(damage);
        }


    }

}
