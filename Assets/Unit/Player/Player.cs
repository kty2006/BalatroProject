using System;
using UnityEngine;

public class Player : Unit, IEvent
{
    public override void Awake()
    {
        base.Awake();
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }
}

public class PlayerAttack : IAttack, IEvent
{
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log("�÷��̾����");
    }
}

public class PlayerMove : IMove, IEvent
{
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log("������");
    }
}


