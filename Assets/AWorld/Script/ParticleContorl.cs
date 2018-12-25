using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleContorl
{


    static GameObject Play(GameObject perfab, GameObject parent)
    {
        perfab = GameObject.Instantiate<GameObject>(perfab);

        if (parent != null)
        {
            perfab.transform.SetParent(parent.transform, false);
        }

        if (!perfab.activeSelf)
        {
            perfab.SetActive(true);
        }

        return perfab;
    }

    static GameObject Play(GameObject perfab, GameObject parent, Vector3 pos, Quaternion rota, Vector3 scz)
    {
        perfab = Play(perfab, parent);

        perfab.transform.position = pos;
        perfab.transform.rotation = rota;
        perfab.transform.localScale = scz;

        return perfab;
    }


    static public GameObject Play(ParticleType type, GameObject parent, Vector3 pos, Quaternion rota, Vector3 scz)
    {
        return Play(GetResources(type), parent, pos, rota, scz);
    }

    static public GameObject Play(ParticleType type, GameObject parent, Transform transform)
    {
        return Play(GetResources(type), parent, transform.position, transform.rotation, transform.localScale);
    }

    static public GameObject Play(ParticleType type, Vector3 pos, Quaternion rota, Vector3 scz)
    {
        return Play(GetResources(type), GameObject.Find("ParticleGround"), pos, rota, scz);
    }

    static public GameObject Play(ParticleType type, Transform transform)
    {
        return Play(GetResources(type), GameObject.Find("ParticleGround"), transform.position, transform.rotation, transform.localScale);
    }

    static public GameObject Play(ParticleType type, GameObject parent)
    {
        return Play(GetResources(type), parent);
    }

    static public GameObject Play(ParticleType type)
    {
        return Play(GetResources(type), GameObject.Find("ParticleGround"));
    }


    static GameObject GetResources(ParticleType type)
    {
        switch (type)
        {
            case ParticleType.fire: return Resources.Load<GameObject>("Parcitle/WFXMR_MF FPS RIFLE2");
            case ParticleType.boom: return Resources.Load<GameObject>("Parcitle/WFXMR_Explosion Simple");
            case ParticleType.Blade: return Resources.Load<GameObject>("Parcitle/WFXMR_Explosion StarSmoke");
            default: return null;
        }

    }


}

public enum ParticleType
{
    boom,
    fire,
    Blade,

}