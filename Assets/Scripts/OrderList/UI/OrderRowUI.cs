using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CafeGame;

public class OrderRowUI : MonoBehaviour
{
    [SerializeField]GameObject orderStart;
    [SerializeField]GameObject orderEnd;

    public void UpdateRow(Order order){
        orderStart.GetComponentInChildren<OrderCellUI_Customer>().UpdateCell(order);
        if(order.Cake != null){
            orderEnd.GetComponentInChildren<OrderCellUI_Food>().UpdateCell(order);
            orderEnd.GetComponentInChildren<OrderCellUI_Satisfaction>().UpdateCell(order);
        }
        else{
            Destroy(orderEnd);
        }
    }
}
