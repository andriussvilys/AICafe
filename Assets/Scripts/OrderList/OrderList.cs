using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CafeGame{
    public class OrderList : MonoBehaviour
    {
        [SerializeField] OrderListEventSO orderUIChannel;

        [SerializeField] OrderEventSO orderChannel;

        public List<Order> orders = new List<Order>();
        public List<Order> List {get{return orders;} set{orders = new List<Order>();}}

        private void OnEnable() {
            orderChannel.OnEventRaise += UpdateList;
        }

        private void OnDisable() {
            orderChannel.OnEventRaise -= UpdateList;
        }

        public void UpdateList(Order order)
        {
            Order existingOrder = orders.Find(elem => elem.Customer.StyleVector.SequenceEqual(order.Customer.StyleVector));
            if(existingOrder == null){
                orders.Add(order);
            }
            else{
                int index = orders.IndexOf(existingOrder);
                orders[index] = order;
            }
            orderUIChannel.RaiseEvent(orders);

        }
    }
}
