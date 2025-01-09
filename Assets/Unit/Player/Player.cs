using System;
using UnityEngine;

public class Player : Unit, IEvent
{
    public override void Awake()
    {
        base.Awake();
        turnSystem = new PlayerTurnSystem();
        Local.EventHandler.Register<PlayerAttack>((att) => { turnSystem.ActionSetting(new PlayerAttack()); });
        Local.EventHandler.Register<PlayerMove>((att) => { turnSystem.ActionSetting(new PlayerMove()); });
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}

public class PlayerAttack : IAttack, IEvent
{
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log("공격");
    }
}

public class PlayerMove : IMove, IEvent
{
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log("움직임");
    }
}

public class PlayerTurnSystem : UnitTurnSystem, Observer
{
    public UnitType unitType = new PlayerAttack();


    public void Update()
    {
        throw new NotImplementedException();
    }

}
