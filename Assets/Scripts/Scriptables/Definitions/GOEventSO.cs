using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameObject Event")]
public class GOEventSO : ScriptableObject
{
    public UnityAction<GameObject, bool> OnEventRaise;

    public void RaiseEvent(GameObject GO, bool value){
        OnEventRaise?.Invoke(GO, value);
    }

}