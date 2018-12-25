using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UISelectBUFF : MonoBehaviour
{
    public CannonTowerMono Player;

    public GameObject BuffPlane_0;
    public GameObject BuffPlane_1;
    public GameObject BuffPlane_2;
    public GameObject SelectButton;


    public Image Plane_0;
    public Image Plane_1;
    public Image Plane_2;

    public Text Name_0;
    public Text Name_1;
    public Text Name_2;

    public Text Content_0;
    public Text Content_1;
    public Text Content_2;

    public Text ButtonText;


    //Dictionary<string, AttackBUFF> BuffDic = new Dictionary<string, AttackBUFF>();
    public List<CannonBUFF> BuffList;
    // Use this for initialization
    void Start()
    {
        //InitBuffList();

    }

    public void InitBuffList()
    {
        BuffList = new List<CannonBUFF>();

        BuffList.Add(gameObject.AddComponent<SuckMoneyBuff>());
        BuffList.Add(gameObject.AddComponent<EnergyRecoveryBuff>());
        BuffList.Add(gameObject.AddComponent<BalanceBuff>());

        BuffList.Add(gameObject.AddComponent<CritBuff>());
        BuffList.Add(gameObject.AddComponent<VampireBUFF>());
        
        BuffList.Add(gameObject.AddComponent<AngerBuff>());
        BuffList.Add(gameObject.AddComponent<RichBuff>());

        BuffList.Add(gameObject.AddComponent<RotateSpeedBuff>());
        
        BuffList.Add(gameObject.AddComponent<EnergyOverloadBuff>());
        BuffList.Add(gameObject.AddComponent<ReplaceThisBuff>());
        BuffList.Add(gameObject.AddComponent<KillCountMoneyBuff>());

        BuffList.Add(gameObject.AddComponent<RigidityBuff>());
        BuffList.Add(gameObject.AddComponent<MoneyCritBuff>());
        BuffList.Add(gameObject.AddComponent<RandomMoneyBuff>());
        BuffList.Add(gameObject.AddComponent<AttackSputteringBuff>());
        BuffList.Add(gameObject.AddComponent<IceBulletsBuff>());
        BuffList.Add(gameObject.AddComponent<BoilingBuff>());

        BuffList.Add(gameObject.AddComponent<WrathBuff>());
        BuffList.Add(gameObject.AddComponent<LightningBuff>());
        BuffList.Add(gameObject.AddComponent<BladeBulletBuff>());
        BuffList.Add(gameObject.AddComponent<FlamesBuff>());
        BuffList.Add(gameObject.AddComponent<EssenceOfLifeBuff>());
    }


    void ShowUI()
    {
        BuffPlane_0.SetActive(true);
        BuffPlane_1.SetActive(true);
        BuffPlane_2.SetActive(true);
        SelectButton.SetActive(true);
    }

    public void HidendUI()
    {
        BuffPlane_0.SetActive(false);
        BuffPlane_1.SetActive(false);
        BuffPlane_2.SetActive(false);
        SelectButton.SetActive(false);
    }


    //Dictionary<string, AttackBUFF> RandomBuffDic = new Dictionary<string, AttackBUFF>();
    List<CannonBUFF> RandomBuffList;
    public int LevelPoint = 0;

    public void RandomBuff()
    {

        if (SelectButton.activeSelf)
        {
            LevelPoint++;
        }
        else
        {
            ShowUI();

            RandomBuffList = new List<CannonBUFF>();

            if (Player.Attritube.GetIntger(UnitStaticAttritubeType.Level)==1)
            {
                RandomBuffList.Add(BuffList[0]);
                RandomBuffList.Add(BuffList[1]);
                RandomBuffList.Add(BuffList[2]);
            }
            else
            {
                PutRandomBuff();
            }


            DisplsyBuffList();
        }

        if (LevelPoint > 0)
        {
            ButtonText.text = string.Format("确定选择({0})", LevelPoint + 1);
        }
        else
        {
            ButtonText.text = "确定选择";
        }


    }

    public void PutRandomBuff()
    {
        List<CannonBUFF> copyList = new List<CannonBUFF>(BuffList);

        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, copyList.Count);
            RandomBuffList.Add(copyList[index]);
            copyList.RemoveAt(index);
        }
    }

    public void DisplsyBuffList()
    {
        Name_0.text = RandomBuffList[0].ChinName;
        Name_1.text = RandomBuffList[1].ChinName;
        Name_2.text = RandomBuffList[2].ChinName;

        Content_0.text = RandomBuffList[0].Introduce;
        Content_1.text = RandomBuffList[1].Introduce;
        Content_2.text = RandomBuffList[2].Introduce;
    }

    // Buff 类型  即时，重复

    int Index = 0;

    public void EnterSelectEvent()
    {
        List<CannonBUFF> list = Player.GetBuffList();

        CannonBUFF buff = null;


        if (RandomBuffList[Index].Immediately)
        {
            RandomBuffList[Index].DoBUFF(Player, null);
        }
        else
        {
            foreach (var item in list)
            {
                if (item.Name == RandomBuffList[Index].Name)
                {
                    buff = item;
                    break;
                }
            }

            if (buff != null)
            {
                buff.Add(RandomBuffList[Index]);
            }
            else
            {
                Player.InitBuff(RandomBuffList[Index]);
            }
        }

        if (!RandomBuffList[Index].Repeat)
        {
            BuffList.Remove(RandomBuffList[Index]);
        }

        HidendUI();

        if (LevelPoint > 0)
        {
            LevelPoint--;
            RandomBuff();
        }

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

        if (!NewBuff.Repeat)
        {
            BuffList.Remove(NewBuff);
        }
    }



    public void Plane_0_Onclick()
    {
        Index = 0;
        SelectSetFalse(Plane_0);
    }

    public void Plane_1_Onclick()
    {
        Index = 1;
        SelectSetFalse(Plane_1);
    }

    public void Plane_2_Onclick()
    {
        Index = 2;
        SelectSetFalse(Plane_2);
    }

    void SelectSetFalse(Image image)
    {
        Plane_1.enabled = false;
        Plane_0.enabled = false;
        Plane_2.enabled = false;

        image.enabled = true;
    }
}
