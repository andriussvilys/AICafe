using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/BoolString Event")]
public class BoolStringEvent_SO : ScriptableObject
{
    public UnityAction<bool, string> OnEventRaise;

    public void RaiseEvent(bool show, string str=""){
        OnEventRaise?.Invoke(show, str);
    }

}
