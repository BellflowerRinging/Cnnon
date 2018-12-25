using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonGameContorl : MonoBehaviour
{

    public UIEvent UIContorl;
    public UISelectBUFF UISelectBuffContorl;
    public BuffListWindowContorl BuffListWindowContorl;
    public GameOverWindowContorl GameOverWindowContorl;

    public Text UIKillCount;

    public GameObject Player { get; private set; }
    public CannonTowerMono PlayerUnit { get; private set; }

    public GameObject PlayerCamera;
    public GameObject MenuCamera;

    public GameObject GamingUI;
    public GameObject MenuUI;


    public float DcrUpdataTime;

    public float CreateDcrOnceTime;

    public float Distent;

    public float HaveMoney = 0f;

    public float DcrLevelHpMult = 0.05f;

    public float LevelMpAdd = 10f;

    [SerializeField] public AttackSpeedLevelSetting AttackSpeedLevelSetting;
    [SerializeField] public AttackLevelSetting AttackLevelSetting;
    [SerializeField] public SingelKillData SingelKillData;


    Vector3 MenuCameraPostion;
    Quaternion MenuCameraRotation;

    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 2f;

        MenuCameraPostion = MenuCamera.transform.position;
        MenuCameraRotation = MenuCamera.transform.rotation;

        //UISelectBuffContorl.InitBuffList();

        //BuffListWindowContorl.InitBuffListWindow();

        InitBuffList();
    }

    public List<CannonBUFF> BuffList;

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

        UISelectBuffContorl.BuffList = BuffList;

        BuffListWindowContorl.BuffList = BuffList;
    }

    public void Play()
    {
        InitGame();

        StartCoroutine(CameraToGameing(MenuCamera, PlayerCamera));
    }

    private float BackupCreateDcrOnceTime = -1f;

    public void InitGame()
    {
        SetPlayer(GameObject.Find("PlayerGo/CannonTower"));

        if (BackupCreateDcrOnceTime == -1f) BackupCreateDcrOnceTime = CreateDcrOnceTime;
        else CreateDcrOnceTime = BackupCreateDcrOnceTime;

        //GameOver();
        DcrList = new List<GameObject>();

        SetCoroutine();

        UIContorl.InitUI();

        PlayerUnit.PlayerInit();

        SetMoney(0);

        SetKillCount(0);

        _DcrHp = (new UnitAttritube(AttritubeAccess.GetAttrDic("BigDcr"), typeof(UnitStaticAttritubeType))).GetFloat(UnitStaticAttritubeType.MaxHp);

        UISelectBuffContorl.HidendUI();

        UISelectBuffContorl.InitBuffList();
    }

    public void SetPlayer(GameObject player)
    {
        Player = player;
        PlayerUnit = player.GetComponent<CannonTowerMono>();
    }

    public void SetCoroutine()
    {
        /*if (CreateDcrIE == null)
        {
            CreateDcrIE = CreateDcr();
        }

        if (CreateTimeDecreaseIE == null)
        {
            CreateTimeDecreaseIE = CreateTimeDecrease();
        }

        if (SelectAttackTargetIE == null)
        {
            SelectAttackTargetIE = SelectAttackTarget();
        }*/

        CreateDcrIE = CreateDcr();
        CreateTimeDecreaseIE = CreateTimeDecrease();
        SelectAttackTargetIE = SelectAttackTarget();


    }

    IEnumerator CreateDcrIE;
    IEnumerator CreateTimeDecreaseIE;
    IEnumerator SelectAttackTargetIE;

    public void StartGame()
    {
        StartCoroutine(CreateDcrIE);
        StartCoroutine(CreateTimeDecreaseIE);
        StartCoroutine(SelectAttackTargetIE);
    }

    public void GameOver()
    {
        StopGame();

        GameOverWindowContorl.Open();

        if (DcrList != null)
            foreach (var item in DcrList)
            {
                if (item != null)
                {
                    item.GetComponent<UnitMonoBehaciour>().Death();
                }
            }

    }

    public void BackToMenu()
    {
        StartCoroutine(CameraToMenuing(MenuCamera, MenuCameraPostion, MenuCameraRotation));
    }

    public void StopGame()
    {
        if (CreateDcrIE != null)
        {
            StopCoroutine(CreateDcrIE);
        }
        if (CreateTimeDecreaseIE != null)
        {
            StopCoroutine(CreateTimeDecreaseIE);
        }
        if (SelectAttackTargetIE != null)
        {
            StopCoroutine(SelectAttackTargetIE);
        }


        PlayerUnit.StopAttack();
    }

    IEnumerator SelectAttackTarget()
    {
        while (true)
        {
            PlayerUnit.SelectAttackGameObject();
            yield return 5;
        }
    }

    IEnumerator CreateTimeDecrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(DcrUpdataTime);
            Debug.Log("怪物加强了");
            _DcrHp *= (1 + DcrLevelHpMult);
        }
    }

    public List<GameObject> DcrList { get; protected set; }

    public float _DcrHp;
    IEnumerator CreateDcr()
    {
        Vector3 MoveXyz = new Vector3(1, 0, 1);
        Vector3 vector = new Vector3(0, 0, Distent);

        while (true)
        {
            Quaternion rota = Quaternion.Euler(0f, UnityEngine.Random.Range(0, 360), 0f);

            if (PlayerUnit == null) break;

            GameObject dcr = CreateUnitFaceToPlayer(UnitType.BigDcr, rota * vector, PlayerUnit);
            DcrList.Add(dcr);

            IUnit unit = dcr.GetComponent<BigDcrMono>();
            yield return 0;

            unit.Attritube.SetAttr(UnitStaticAttritubeType.MaxHp, _DcrHp);
            unit.Attritube.SetAttr(UnitDynamicAttritubeType.Hp, _DcrHp);

            unit.MoveToPostion(Player.transform.position, MoveXyz);
            yield return new WaitForSeconds(CreateDcrOnceTime);
        }
    }

    GameObject CreateUnitFaceToPlayer(UnitType type, Vector3 vector, UnitMonoBehaciour LookWho)
    {
        GameObject unit = UnitManage.CreateUnit(type, null, LookWho.transform.position + vector);
        Vector3 def = unit.transform.rotation.eulerAngles;
        unit.transform.LookAt(LookWho.gameObject.transform);
        Vector3 the = unit.transform.rotation.eulerAngles;
        unit.transform.rotation = Quaternion.Euler(def.x, the.y, the.z);
        return unit;
    }

    public int KillCount = 0;

    public void DcrDie()
    {
        SetMoney(HaveMoney + SingelKillData.SingelDcrMoney);

        PlayerUnit.MpAdd(SingelKillData.SingelDcrMp);

        SetKillCount(++KillCount);
    }

    public void RandomBuff()
    {
        UISelectBuffContorl.RandomBuff();
    }

    public void SetMoney(float money)
    {
        money = Mathf.Clamp(money, 0, 99999);

        HaveMoney = money;

        UIContorl.SetMoney(HaveMoney);
    }

    public void SetMoney()
    {
        UIContorl.SetMoney(HaveMoney);
    }

    public void SetKillCount(int value)
    {
        KillCount = value;
        UIKillCount.text = "击杀数：" + value;
    }



    public float CameraTime;

    GameObject big;

    IEnumerator CameraToGameing(GameObject camera, GameObject toCamera)
    {
        float startTime = Time.time;
        PlayerUnit._Actions.Idle();

        if (big == null)
        {
            big = GameObject.Find("Big");
        }

        big.SetActive(false);

        MenuUI.SetActive(false);

        while (true)
        {
            Vector3 toPos = toCamera.transform.position;
            Quaternion toRota = toCamera.transform.rotation;

            camera.transform.position = Vector3.Slerp(camera.transform.position, toPos, (Time.time - startTime) / CameraTime);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, toRota, (Time.time - startTime) / CameraTime);

            if (V3Equals(camera.transform.position, toPos))
            {
                break;
            }

            yield return 0;
        }

        Debug.Log("调整完毕 开始游戏");

        MenuCamera.SetActive(false);

        GamingUI.SetActive(true);

        PlayerCamera.SetActive(true);

        StartGame();
    }

    IEnumerator CameraToMenuing(GameObject camera,Vector3 pos,Quaternion rota)
    {
        float startTime = Time.time;

        MenuCamera.SetActive(true);

        GamingUI.SetActive(false);

        PlayerCamera.SetActive(false);

        while (true)
        {
            camera.transform.position = Vector3.Slerp(camera.transform.position, pos, (Time.time - startTime) / CameraTime);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, rota, (Time.time - startTime) / CameraTime);

            if (V3Equals(camera.transform.position, pos))
            {
                break;
            }

            yield return 0;
        }

        MenuUI.SetActive(true);

        big.SetActive(true);

        PlayerUnit._PlayerStateBarContorl.SetHpBarLength(1);

        PlayerUnit._Actions.MenuIdel();
    }

    bool V3Equals(Vector3 a,Vector3 b)
    {
        if (Math.Round(a.x, 2) == Math.Round(b.x, 2) &&
            Math.Round(a.y, 2) == Math.Round(b.y, 2) &&
            Math.Round(a.z, 2) == Math.Round(b.z, 2) )
        {
            return true;
        }

        return false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    /*bool QuaternionEquals(Quaternion a, Quaternion b)
    {
        if (Math.Round(a.x, 1) == Math.Round(b.x, 1) &&
            Math.Round(a.y, 1) == Math.Round(b.y, 1) &&
            Math.Round(a.z, 1) == Math.Round(b.z, 1) &&
            Math.Round(a.w, 1) == Math.Round(b.w, 1))
        {
            return true;
        }

        return false;
    }*/
}


[Serializable]
public class AttackSpeedLevelSetting
{
    public float Max = 2f;
    public float Min = 0.1f;
    public float InitMoney = 20f;
    public float LevelUpMult = 0.95f;
    public float LevelUpMoneyMult = 1.1f;
}

[Serializable]
public class AttackLevelSetting
{
    public float InitMoney = 20f;
    public float LevelUpMult = 1.1f;
    public float LevelUpMoneyMult = 1.1f;
}

[Serializable]
public class SingelKillData
{
    public float SingelDcrMoney = 10f;

    public float SingelDcrMp = 5f;

    public float SingelDcrDamageMult = 1f;
}