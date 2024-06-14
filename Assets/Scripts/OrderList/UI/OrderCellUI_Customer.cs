using UnityEngine.UI;
using CafeGame;

public class OrderCellUI_Customer : OrderCellUI
{
    public override void UpdateCell(Order order)
    {
        content.GetComponent<RawImage>().texture = order.Customer.Texture;
    }
}
