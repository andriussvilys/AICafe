using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/OrderList Event")]
public class OrderListEventSO : ScriptableObject
{
    public UnityAction<List<CafeGame.Order>> OnEventRaise;

    public void RaiseEvent(List<CafeGame.Order> orderList){
        OnEventRaise?.Invoke(orderList);
    }

}


