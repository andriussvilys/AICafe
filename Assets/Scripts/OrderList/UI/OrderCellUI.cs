using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CafeGame;
public abstract class OrderCellUI : MonoBehaviour
{
    [SerializeField]
    protected GameObject content;

    public abstract void UpdateCell(Order order);
}
