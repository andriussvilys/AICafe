using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CafeGame;

public class OrdersUI : Menu
{
    [SerializeField]
    OrderListEventSO ordersChannel;

    [SerializeField]
    OrderRowUI orderRowPrefab;

    [SerializeField]
    VerticalLayoutGroup panel; 

    private bool _isVisible = false;

    KeyCode callButton = KeyCode.I;  

    private void Update() {
        if (Input.GetKeyDown(callButton))
        {
            OnActivate();
        }
    }

    private void OnActivate(){
        if(!_isVisible){
            Enter(()=>{
                _isVisible = true;
            });
        }
        else{
            Leave(()=>{
                _isVisible = false;
            });
        }
    }

    private void OnEnable() {
        ordersChannel.OnEventRaise += Render;
    }

    private void OnDisable() {
        ordersChannel.OnEventRaise -= Render;
    }

    private void Render(List<Order> orders){
        ClearPanel();
        foreach (Order order in orders)
        {
            OrderRowUI newRow = Instantiate(orderRowPrefab, new Vector2(), new Quaternion(), panel.transform);
            newRow.UpdateRow(order);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(panel.GetComponent<RectTransform>());
    }

    private void ClearPanel(){
        foreach(Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
