using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMessage : MonoBehaviour {


    public Collider Enter {get; private set; }

    private void OnTriggerEnter(Collider collider)
    {
        Enter = collider;
    }

    public void ResetCollision()
    {
        Enter = null;
    }

}
