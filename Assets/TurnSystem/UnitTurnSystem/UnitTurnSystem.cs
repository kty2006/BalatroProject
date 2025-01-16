using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class UnitTurnSystem : MonoBehaviour, ITurnObj, IEvent
{
    protected Action[] TurnAction;//�� ���� ������ �׼�

    public virtual void Start()
    {
        Local.TurnSystem.Register(this);
    }

    public bool Invoke()//�׼� ����
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

    public void Register(UnitBehaviour unitType)//�׼� �Ҵ�
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

    public void UnRegister(UnitBehaviour unitType)//�׼� ����
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

    public void SetTurnLength(int Length)//���Ͽ� ������ �׼� ���� ����
    {
        TurnAction = new Action[Length];
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
