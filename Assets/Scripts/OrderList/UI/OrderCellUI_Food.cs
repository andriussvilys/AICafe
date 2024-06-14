using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CafeGame;

public class OrderCellUI_Food : OrderCellUI
{
    public override void UpdateCell(Order order)
    {
        content.GetComponent<RawImage>().texture = order.Cake.Texture;
    }
}
