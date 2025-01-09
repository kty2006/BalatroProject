using System;
using UnityEngine;

public class UnitTurnSystem : ITurnObj, IEvent
{
    private Action TurnAction;
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


    public UnitTurnSystem ActionSetting(UnitType unitType) //빌더 패턴으로 개발 --플레이대상 -> 행동 -> 버프 ㄷㄷ
    {
        Register(unitType.Invoke);
        Debug.Log("등로ㅓㄱ");
        return this;
    }
    public UnitTurnSystem SettingEnd<TurnTarget>(UnitTurnSystem turnTarget)
    {
        Local.EventHandler.Register<TurnTarget>((Pu) => { Local.TurnSystem.Register(turnTarget);});
        Debug.Log("등로ㅓㄱ2");
        return this;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
