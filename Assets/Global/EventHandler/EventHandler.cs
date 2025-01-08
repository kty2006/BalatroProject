using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class EventHandler 
{
    private readonly Dictionary<Type, EventContainer> GameEvents = new(); //직접 이벤트를 사용하기 위한 자료구조

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
    public HashSet<EventWrapper> EventWrapperSet =new(); //Action을 담기 위핸 자료구조(Action 반환값이 제네릭이기에 액션 자체적으로 여러 함수를 담을수 없음)

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

public abstract class EventWrapper //확장을 위한 추상 클래스
{
    public abstract void Invoke(IEvent ev);
    public abstract bool EqualEvent(object ev);
}

public class EventWrapper<TEvent> : EventWrapper //  원하는 매개변수로 받을수 있게 Action을 제네릭 클래스로 덮음
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
