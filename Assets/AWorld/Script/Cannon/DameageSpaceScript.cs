using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameageSpaceScript : MonoBehaviour {

   public  CannonTowerMono _CannonTowerMono;
    public CannonGameContorl Game;

    private void Start()
    {
        Game = GameObject.Find("GameContorl").GetComponent<CannonGameContorl>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Dcr"))
        {
            BigDcrMono dcr = other.gameObject.GetComponent<BigDcrMono>();
            _CannonTowerMono.Damage(dcr.Attritube.GetFloat(UnitDynamicAttritubeType.Hp)* Game.SingelKillData.SingelDcrDamageMult);
            Destroy(other.gameObject);
        }

    }

}
