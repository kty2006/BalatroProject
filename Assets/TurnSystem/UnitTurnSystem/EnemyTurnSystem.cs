using UnityEngine;
using Cysharp.Threading.Tasks;


[DefaultExecutionOrder(0)]
public class EnemyTurnSystem : UnitTurnSystem
{
    public override void Start()
    {
        base.Start();
        SetTurnLength(3);

    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            this.Register(new EnemyAttack());
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Local.TurnSystem.TurnStart(true);
            
        }
    }
}
