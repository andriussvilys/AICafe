using UnityEngine;
using UnityEngine.Events;
using CafeGame;

[CreateAssetMenu(menuName = "Events/Order Event")]
public class OrderEventSO : ScriptableObject
{
    public UnityAction<Order> OnEventRaise;

    public void RaiseEvent(Order order){
        OnEventRaise?.Invoke(order);
    }

}