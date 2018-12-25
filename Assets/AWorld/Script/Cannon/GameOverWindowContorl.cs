using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverWindowContorl : WindowContorl
{
    public CannonGameContorl Game;

    private void Start()
    {

    }

    public void Again()
    {
        Game.Play();
        base.Colse();
    }

    public void BackToMenu()
    {
        Game.BackToMenu();
        base.Colse();
    }
}
