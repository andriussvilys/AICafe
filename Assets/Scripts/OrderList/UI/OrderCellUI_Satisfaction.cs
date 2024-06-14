using UnityEngine;
using CafeGame;

public class OrderCellUI_Satisfaction : OrderCellUI
{
    public override void UpdateCell(Order order)
    {
        float satisfaction = (float)((order.Satisfaction + 1.0) * 50.0);
        satisfaction = Mathf.Round(satisfaction * 100) / 100f;
        content.GetComponent<TMPro.TextMeshProUGUI>().text = satisfaction.ToString() + " / 100";
    }
}
