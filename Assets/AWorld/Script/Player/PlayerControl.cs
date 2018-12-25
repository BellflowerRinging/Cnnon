using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent (typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerBehaviour))]
public class PlayerControl : MonoBehaviour
{
    public Animator _Animator;
    public DebugLabel _Debug;
    public List<BallContorl> _BallContorlList;

    PlayerState PlayerState;


    public PlayerState GetPlayerState()
    {

        string name;

        if (_Animator.IsInTransition(0))
        {
            name = _Animator.GetNextAnimatorClipInfo(0)[0].clip.name;
        }
        else
        {
            name = _Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }

        switch (name)
        {
            case "Idle":
            case "Attackstandy":
                PlayerState = PlayerState.Idel;
                break;

            case "Walk":
                PlayerState = PlayerState.Walk;
                break;

            case "Run00":
                PlayerState = PlayerState.Run;
                break;

            case "Jump":
            case "Jump_Dogger":
                PlayerState = PlayerState.Jump;
                break;

            case "Attack02":
            case "Attack01":
                PlayerState = PlayerState.Attack;
                break;

            default:
                PlayerState = PlayerState.None;
                break;

        }

        _Debug.AddItem("PlayerState",Enum.GetName(typeof(PlayerState), PlayerState));
        return PlayerState;
    }

    public void ClearBallContorlTouchId()
    {
        foreach (var item in _BallContorlList)
        {
            item.DisTouchId();
        }
    }

}

public enum PlayerState
{
    None,
    Walk,
    Run,
    Idel,
    Jump,
    Attack
}

public enum _AnimState
{
    Idel = 0,
    Walk = 1,
    Run00 = 2,
    Run = 3,
    Jump = 4,
    AttackStandy = 5,
    Attack = 6,
    Attack_2 = 7,
}