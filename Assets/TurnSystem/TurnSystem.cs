using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TurnSystem
{
    private List<ITurnObj> Turn = new(); //
    public UnitType unitType;

    public void Register(ITurnObj turn) //
    {
        Turn.Add(turn);
    }

    public void UnRegister() //
    {
       Turn.Remove(Turn.FirstOrDefault<ITurnObj>());
    }

    public async UniTask Invoke() //
    {
        Debug.Log(Turn.Count);
        for (int i = 0; i < Turn.Count; i++)
        {
            Turn[i].Invoke();
            await UniTask.WaitUntil(() => true);
        }
        Turn.Clear();
    }

}
