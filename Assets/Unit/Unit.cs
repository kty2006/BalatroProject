using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitTurnSystem turnSystem;

    public virtual void Awake()
    {
        Local.EventHandler.Register<Unit>((unit) => { Local.TurnSystem.UnRegister(); });
    }
}
