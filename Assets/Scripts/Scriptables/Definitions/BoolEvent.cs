using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Bool Event")]
public class BoolEvent : ScriptableObject
{
    private int _subscriberCount = 0;
    public int SubscriberCount {get {return _subscriberCount;}}
    
    private UnityAction<bool> OnEventRaise;

    public void RaiseEvent(bool value){
        OnEventRaise?.Invoke(value);
    }

    public void Subscribe(UnityAction<bool> action){
        ++_subscriberCount;
        OnEventRaise += action;
    }

    public void Unsubscribe(UnityAction<bool> action){
        --_subscriberCount;
        OnEventRaise -= action;
    }

}
