using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TurnSystem
{
    private List<ITurnObj> turns = new(); //턴을 가지고 있는 리스트
    private bool turnproress = false;
    public UnitType unitType;

    public void Register(ITurnObj turn) //턴 할당
    {
        turns.Add(turn);
    }

    public void UnRegister() //턴 해제
    {
        turns.Remove(turns.FirstOrDefault<ITurnObj>());
    }

    public bool TurnStart(bool turnstart)//
    {
        return turnproress = turnstart;
    }

    public async UniTask Invoke() //턴 실행
    {
        for (int i = 0; i < turns.Count; i++)
        {
            await UniTask.WaitUntil(() => turnproress);
            turns[i].Invoke();
            TurnStart(false);
        }
        turns.Clear();
        Debug.Log("턴 종료");
    }

}
