using System;
using UnityEngine;

public class Enemy : Unit
{
}

public class EnemyAttack : IAttack, IEvent
{
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log("РћАјАн");
    }
}
