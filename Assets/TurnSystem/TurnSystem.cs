using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TurnSystem
{
    private List<ITurnObj> turns = new(); //���� ������ �ִ� ����Ʈ
    private bool turnproress = false;
    public UnitType unitType;

    public void Register(ITurnObj turn) //�� �Ҵ�
    {
        turns.Add(turn);
    }

    public void UnRegister() //�� ����
    {
        turns.Remove(turns.FirstOrDefault<ITurnObj>());
    }

    public bool TurnStart(bool turnstart)//
    {
        return turnproress = turnstart;
    }

    public async UniTask Invoke() //�� ����
    {
        for (int i = 0; i < turns.Count; i++)
        {
            await UniTask.WaitUntil(() => turnproress);
            turns[i].Invoke();
            TurnStart(false);
        }
        turns.Clear();
        Debug.Log("�� ����");
    }

}
