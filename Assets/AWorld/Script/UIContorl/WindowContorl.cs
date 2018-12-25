using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowContorl : MonoBehaviour {

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Colse()
    {
        gameObject.SetActive(false);
    }

    public virtual void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
