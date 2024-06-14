using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/String Event")]
public class StringEvent_SO : ScriptableObject
{
    public UnityAction<string> OnEventRaise;

    public void RaiseEvent(string str){
        OnEventRaise?.Invoke(str);
    }

}