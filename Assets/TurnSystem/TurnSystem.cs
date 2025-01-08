using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
public class TurnSystem
{
    private List<ITurnObj> Turn = new(); //
    public UnitType unitType;

    public void Register(ITurnObj turn) //
    {
        Turn.Add(turn);
    }

    public async UniTask Invoke() //
    {
        for (int i = 0; i < Turn.Count; i++)
        {
            Turn[i].Invoke();
            await UniTask.WaitUntil(() => true);
        }
        Turn.Clear();
    }

    public void TurnSetting(UnitTurnSystem turnSystem) //���� �������� ���� --�÷��̴�� -> �ൿ -> ���� ����
    {
        turnSystem.Register(unitType.Execute);
        Local.EventHandler.Register<PlayerTurnSystem>((Pu) => { Local.TurnSystem.Register(turnSystem); });
    }
}
