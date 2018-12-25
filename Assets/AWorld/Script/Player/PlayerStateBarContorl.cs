using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBarContorl : MonoBehaviour
{

    public Image HeardImage;
    public Image HpBar;
    public Image MpBar;
    public Text Name;
    public Text Level;


    Coroutine _HpCoro;

    public void SetHpBarLength(float Percent)
    {
        if (_HpCoro != null) StopCoroutine(_HpCoro);

        _HpCoro = StartCoroutine(SlowBar(HpBar, Percent));
    }

    Coroutine _MpCoro;

    public void SetMpBarLength(float Percent)
    {
        if (_MpCoro != null) StopCoroutine(_MpCoro);

        _MpCoro = StartCoroutine(SlowBar(MpBar, Percent));
    }

    IEnumerator SlowBar(Image Bar, float Percent)
    {
        Percent = Mathf.Clamp(Percent, 0f, 1f);

        float speed = 1f * Math.Abs(Percent - Bar.fillAmount);

        float val = Math.Sign(Percent - Bar.fillAmount) * speed * Time.fixedDeltaTime;

        while (Bar.fillAmount != Percent)
        {
            Bar.fillAmount += val;

            if (val > 0 && Bar.fillAmount > Percent)
            {
                Bar.fillAmount = Percent;
            }
            else if (val < 0 && Bar.fillAmount < Percent)
            {
                Bar.fillAmount = Percent;
            }

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    public void SetNameText(string _Name)
    {
        Name.text = _Name;
    }

    public void SetLevelText(int _Level)
    {
        Level.text = _Level + "";
    }
}
