using UnityEngine;
using System.Collections;

public class CannonCamera : MonoBehaviour
{
    public GameObject Cannon;

    private void LateUpdate()
    {
        transform.LookAt(Cannon.transform);
    }
}
