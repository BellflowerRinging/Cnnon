using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public PlayerBehaviour _PlayerBehaviour;

    private void Start()
    {
       
    }

    bool AttackA = false;
    bool AttackB = false;


    void Update()
    {

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform==RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButton(0))
            {
                AttackA = true;
            }


            if (Input.GetMouseButton(1))
            {
                AttackB = true;
            }
        }

        if (AttackA)
        {
            _PlayerBehaviour.Attack(0);
            AttackA = false;
        }


        if (AttackB)
        {
            _PlayerBehaviour.Attack(1);
            AttackB = false;
        }
    }

    public void PlayerAttackA()
    {
        AttackA = true;
    }

    public void PlayerAttackB()
    {
        AttackB = true;
    }

}
