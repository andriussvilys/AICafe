using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CafeGame;

[CreateAssetMenu(menuName = "Events/FoodList Event")]
public class FoodListEventSO : ScriptableObject
{
    public UnityAction<List<Food>> OnEventRaise;

    public void RaiseEvent(List<Food> foodList){
        OnEventRaise?.Invoke(foodList);
    }

}
