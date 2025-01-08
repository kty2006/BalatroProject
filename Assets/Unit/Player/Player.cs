using System;
using UnityEngine;

public class Player : Unit, IEvent
{
    public IAttack playerAttack = new PlayerAttack();

    public void Awake()
    {
    }



    public void Execute()
    {
    }
}

public class PlayerAttack : IAttack
{


    public void Execute()
    {
        Debug.Log("김태연 아이큐 180");
    }
}

public class PlayerTurnSystem : IEvent
{
    
    public void Execute()
    { }
}
