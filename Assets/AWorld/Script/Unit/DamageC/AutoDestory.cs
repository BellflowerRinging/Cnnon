using UnityEngine;
using System.Collections;

public class AutoDestory : MonoBehaviour
{
    public Camera _Camera;
    public float Time=1f;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Destory());
        _Camera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        transform.LookAt(_Camera.transform);
    }

    IEnumerator Destory()
    {
        yield return new WaitForSeconds(Time);
        GameObject.Destroy(gameObject);
    }
}
