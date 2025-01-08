using System;
using UnityEngine;

public class UnitTurnSystem : ITurnObj
{
    public Action TurnAction;
    public bool isMyTurn = false;
    public void Invoke()
    {
        TurnAction?.Invoke();
        EndTurn();
    }

    public void Register(Action action)
    {
        TurnAction += action;
    }

    public void UnRegister(Action action)
    {
        TurnAction -= action;
    }

    public bool EndTurn()
    {
        return isMyTurn = true;
    }

    public virtual void TurnActionFun()
    {

    }
}
