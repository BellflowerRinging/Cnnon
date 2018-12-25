using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DebugButton : MonoBehaviour
{
    public DebugLabel _DebugLabel;
    CannonBUFF buff;
    // Use this for initialization
    void Start()
    {
        buff = gameObject.AddComponent<BoilingBuff>();
    }

    // Update is called once per frame
    void Update()
    {
        //_UnitMonoBehaciour.MoveTo(Vector3.forward *Time.deltaTime);
       // _DebugLabel.AddItem("string", Vector3.Distance(_UnitMonoBehaciour.transform.position,_PlayerBehaviour.transform.position));
    }

    public CannonGameContorl Game;
    public CannonTowerMono Player;
    public UnitMonoBehaciour _UnitMonoBehaciour;



    public void ThisButtonDebuggingModuleWillAbsolutelyNotAppearBUG()
    {
        //_PlayerBehaviour.Damage(20f);
        //CreateUnitFaceToPlayer(UnitType.BigDcr, _PlayerBehaviour);
        //_UnitMonoBehaciour.MoveToPostion(_PlayerBehaviour.transform.position);
        //_UnitMonoBehaciour.Jump();
        //IUnit unit = _UnitMonoBehaciour;
        //unit.TurnToPostion(_PlayerBehaviour.transform.position,Vector3.up);
        //unit.MoveToPostion(_PlayerBehaviour.transform.position, new Vector3(1,0,1));
        //unit.MoveToGameObject(_PlayerBehaviour.gameObject, new Vector3(1, 0, 1));
        //unit.LookAtGameObject(_PlayerBehaviour.gameObject, new Vector3(0, 1, 0),true);
        //unit.Attack(0, _PlayerBehaviour.gameObject);

        /*
        if (Time.timeScale!=0)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }*/

        //Game.RandomBuff();
        ///
        //Turn.TurnToPostion(new Vector3(0,0,0),Vector3.up);


        //Game.Play();
        Game.GameOver();
        Game.BackToMenu();

        //PutBuffInUnit(buff);

    }



    GameObject CreateUnitFaceToPlayer(UnitType type,UnitMonoBehaciour LookWho)
    {
        Vector3 vector = LookWho.GetRotation() * new Vector3(0f, 2f, 5f) * 5f;
        GameObject unit = UnitManage.CreateUnit(type, null, LookWho.transform.position + vector);
        Vector3 def = unit.transform.rotation.eulerAngles;
        unit.transform.LookAt(LookWho.gameObject.transform);
        Vector3 the = unit.transform.rotation.eulerAngles;
        unit.transform.rotation = Quaternion.Euler(def.x, the.y, the.z);
        return unit;
    }


    public void PutBuffInUnit(CannonBUFF NewBuff)
    {
        List<CannonBUFF> list = Player.GetBuffList();

        CannonBUFF buff = null;

        if (NewBuff.Immediately)
        {
            NewBuff.DoBUFF(Player, null);
        }
        else
        {
            foreach (var item in list)
            {
                if (item.Name == NewBuff.Name)
                {
                    buff = item;
                    break;
                }
            }

            if (buff != null)
            {
                buff.Add(NewBuff);
            }
            else
            {
                Player.InitBuff(NewBuff);
            }
        }
    }
}
