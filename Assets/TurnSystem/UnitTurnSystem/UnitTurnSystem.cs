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


    public UnitTurnSystem ActionSetting(UnitType unitType) //���� �������� ���� --�÷��̴�� -> �ൿ -> ���� ����
    {
        Register(unitType.Invoke);
        Debug.Log("��Τä�");
        return this;
    }
    public UnitTurnSystem SettingEnd<TurnTarget>(UnitTurnSystem turnTarget)
    {
        Local.EventHandler.Register<TurnTarget>((Pu) => { Local.TurnSystem.Register(turnTarget);});
        Debug.Log("��Τä�2");
        return this;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
