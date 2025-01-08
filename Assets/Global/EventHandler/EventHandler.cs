using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventHandler 
{
    private readonly Dictionary<Type, EventContainer> GameEvents = new(); //���� �̺�Ʈ�� ����ϱ� ���� �ڷᱸ��

    public void Register<TEvent>(Action<TEvent> onEvent)
    {
        if (!GameEvents.ContainsKey(typeof(TEvent)))
        {
            GameEvents.Add(typeof(TEvent), new EventContainer());
            
        }
        GameEvents[typeof(TEvent)].Register(new EventWrapper<TEvent>(onEvent));
    }
    public void UnRegister<TEvent>(Action<TEvent> onEvent)
    {
        if (!GameEvents.ContainsKey(typeof(TEvent)))
        {
            GameEvents[typeof(TEvent)].UnRegister(onEvent);
        }
    }

    public TEvent Invoke<TEvent>(TEvent ev) where TEvent : IEvent
    {
        if (!GameEvents.ContainsKey(typeof(TEvent)))
        {
            Debug.LogError($"[EventHandler] NotRegisterEvent {nameof(TEvent)}");
            return ev;
        }
        GameEvents[typeof(TEvent)].Invoke(ev);
        return ev;
    }
}
public class EventContainer
{
    public HashSet<EventWrapper> EventWrapperSet =new(); //Action�� ��� ���� �ڷᱸ��(Action ��ȯ���� ���׸��̱⿡ �׼� ��ü������ ���� �Լ��� ������ ����)

    public void Register<TEvent>(EventWrapper<TEvent> eventWrapper)
    {
        EventWrapperSet.Add(eventWrapper);
    }

    public void UnRegister<TEvent>(Action<TEvent> registerEventr)
    {
        foreach (var ev in EventWrapperSet)
        {
            if (ev.EqualEvent(ev))
            {
                EventWrapperSet.Remove(ev);
                return;
            }
        }
    }

    public void Invoke<TEvent>(TEvent ev) where TEvent : IEvent
    {
        foreach (var post in EventWrapperSet)
        {
            post.Invoke(ev);
        }
    }
}

public abstract class EventWrapper //Ȯ���� ���� �߻� Ŭ����
{
    public abstract void Invoke(IEvent ev);
    public abstract bool EqualEvent(object ev);
}

public class EventWrapper<TEvent> : EventWrapper //  ���ϴ� �Ű������� ������ �ְ� Action�� ���׸� Ŭ������ ����
{

    public Action<TEvent> GameEvent;

    public override bool EqualEvent(object ev)
    {
        throw new NotImplementedException();
    }

    public override void Invoke(IEvent ev)
    {
        GameEvent?.Invoke((TEvent)ev);
    }

    public EventWrapper(Action<TEvent> ev)
    {
        GameEvent = ev;
    }

}

public abstract class Event : IEvent
{
    public abstract void Execute();
}
public abstract class Event<TEvent>
{

}
