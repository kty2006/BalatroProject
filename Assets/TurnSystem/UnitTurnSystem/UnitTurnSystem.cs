using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class UnitTurnSystem : MonoBehaviour, ITurnObj, IEvent
{
    protected Action[] TurnAction;//턴 마다 실행할 액션

    public virtual void Start()
    {
        Local.TurnSystem.Register(this);
    }

    public bool Invoke()//액션 실행
    {
        for (int i = 0; i < TurnAction.Length; i++)
        {
            if (TurnAction[i] != null)
            {
                TurnAction[i]?.Invoke();
            }
        }
        return true;
    }

    public void Register(UnitBehaviour unitType)//액션 할당
    {
        for (int i = 0; i < TurnAction.Length; i++)
        {
            if (TurnAction[i] == null)
            {
                TurnAction[i] += unitType.Invoke;
                return;
            }
        }
        Debug.LogError($"[UnitTurnSystem] TurnActionOverflow {nameof(TurnAction)}");
    }

    public void UnRegister(UnitBehaviour unitType)//액션 해제
    {
        for (int i = 0; i < TurnAction.Length; i++)
        {
            if (TurnAction[i] == null)
            {
                TurnAction[i] -= unitType.Invoke;
                return;
            }
        }
    }

    public void SetTurnLength(int Length)//각턴에 실행할 액션 개수 설정
    {
        TurnAction = new Action[Length];
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
