using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarScript : MonoBehaviour {


    public Image HaveHp;
    public Image NotHp;

    [Header("仅更改数值不会刷新Bar")]
    [Range(0, 1)] public float _Percent;

    Camera _PlayerCamera;

    private void Start()
    {
        _PlayerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        SetHpBarLength(_Percent);

        StartCoroutine(updata());
    }

    IEnumerator updata()
    {
        while (true)
        {
            if (_PlayerCamera == null) break;
            transform.LookAt(_PlayerCamera.transform);
            HaveHp.color = new Color(1 - _Percent, _Percent, 0f);
            yield return new WaitForSeconds(Time.deltaTime*10);
        }
    }

    /// <summary>
    /// SetHpBarLength
    /// </summary>
    /// <param name="Percent">0f-1f</param>
    public void SetHpBarLength(float Percent)
    {
        _Percent = Percent;
        Percent = Mathf.Clamp(Percent,0f,1f);
        Percent = 1f - Percent;
        NotHp.fillAmount = Percent;
    }

   
}
