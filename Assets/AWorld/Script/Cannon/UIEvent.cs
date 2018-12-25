using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvent : MonoBehaviour
{
    public CannonGameContorl Game;
    public CannonTowerMono _Player;

    public Text MoneyText;

    public Text AttackText;

    public Text AttackSpeedText;

    private void Start()
    {
        InitUI();
    }

    public void InitUI()
    {
        AttackMoney = Game.AttackLevelSetting.InitMoney;
        AttcakSpeedMoney = Game.AttackSpeedLevelSetting.InitMoney;

        ASLMult = (1 - Game.AttackSpeedLevelSetting.LevelUpMult) * 100f;
        AMult = (Game.AttackLevelSetting.LevelUpMult - 1) * 100f;

        MoneyText.text = string.Format("金币：{0}", Game.HaveMoney);
        AttackText.text = string.Format("攻击+{0}% {1}金币", (int)AMult, (int)AttackMoney);
        AttackSpeedText.text = string.Format("攻速+{0}% {1}金币", (int)ASLMult, (int)AttcakSpeedMoney);
    }

    float AttackMoney;
    float AMult;
    public void AddAttack()
    {
        if (Game.HaveMoney>=AttackMoney)
        {
            Game.HaveMoney -= AttackMoney;
            Game.SetMoney();

            float attack = _Player.Attritube.GetFloat(UnitStaticAttritubeType.Attack);
            _Player.Attritube.SetAttr(UnitStaticAttritubeType.Attack, attack * Game.AttackLevelSetting.LevelUpMult);
            AttackMoney *= Game.AttackLevelSetting.LevelUpMoneyMult;
            AttackText.text = string.Format("攻击+{0}% {1}金币", (int)AMult, (int)Math.Ceiling(AttackMoney));
        }

    }



    float AttcakSpeedMoney;
    float ASLMult;
    public void AddAttackSpeed()
    {
        if (Game.HaveMoney>=AttcakSpeedMoney)
        {
            Game.HaveMoney -= AttcakSpeedMoney;
            Game.SetMoney();

            float speed = _Player.Attritube.GetFloat(UnitStaticAttritubeType.AttackSpeed);
            float AttackIdel = Mathf.Clamp(speed * Game.AttackSpeedLevelSetting.LevelUpMult, Game.AttackSpeedLevelSetting.Min, Game.AttackSpeedLevelSetting.Max); ;
            _Player.Attritube.SetAttr(UnitStaticAttritubeType.AttackSpeed, AttackIdel);
            AttcakSpeedMoney *= Game.AttackSpeedLevelSetting.LevelUpMoneyMult;
            AttackSpeedText.text = string.Format("攻速+{0}% {1}金币", (int)ASLMult, (int)Math.Ceiling(AttcakSpeedMoney));
        }

    }

    public void SetMoney(float money)
    {
        MoneyText.text = string.Format("金币：{0}", Math.Floor(money));
    }

}

