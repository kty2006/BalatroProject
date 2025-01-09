using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerTurnSystem : UnitTurnSystem
{
    public override void Start()
    {
        base.Start();
        SetTurnLength(3);
        Local.EventHandler.Register<PlayerAttack>((att) => { this.Register(new PlayerAttack()); });
        Local.EventHandler.Register<PlayerMove>((mo) => { this.Register(new PlayerMove()); });
    }
}
