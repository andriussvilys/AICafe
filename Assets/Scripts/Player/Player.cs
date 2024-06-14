using System.Collections;
using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using System.Threading.Tasks;

namespace CafeGame{
    public class Player : Persistable
    {
        [SerializeField] Inventory inventory;
        public Inventory Inventory {get{return inventory;}}
        [SerializeField] OrderList orders;
        [SerializeField] GameObject foodPrefab;
        [SerializeField] GanLoader ganLoader_cakes;
        [SerializeField] GanLoader ganLoader_faces;

        float score = 200f;

        protected override async Task<bool> LoadState(SaveState state)
        {
            SaveSystem.Player data = state.player;
            transform.SetPositionAndRotation(data.transform.position, data.transform.rotation);

            List<SaveSystem.GANTexture> inventoryData = data.inventory;
            inventory.List = new List<GANTexture>();
            foreach (SaveSystem.GANTexture textureData in inventoryData)
            {
                GANTexture texture = await ganLoader_cakes.GetTextureAsync(textureData.styleVector);
                inventory.ModifyInventory(texture, true);
            }

            List<SaveSystem.Order> ordersData = data.orders;
            orders.List = new List<Order>();
            foreach (SaveSystem.Order orderData in ordersData)
            {
                Order order;
                GANTexture customerTexture = await ganLoader_faces.GetTextureAsync(orderData.customer.styleVector);
                GANTexture cakeTexture = null;
                if(orderData.cake != null && !UTILS.IsArrayNull(orderData.cake.styleVector)){
                    cakeTexture = await ganLoader_cakes.GetTextureAsync(orderData.cake.styleVector);
                    order = new Order(customerTexture, cakeTexture);
                }
                else{
                    order = new Order(customerTexture);
                }
                orders.UpdateList(order);
            }

            return true;
        }

        protected override Task UpdateState()
        {
            Debug.Log("update player");
            SaveSystem.Player playerData = new SaveSystem.Player(transform, score, inventory.List, orders.List);
            StateManager.SetState(SaveSystem.StateName.Player,  playerData);
            return null;
        }

        public void ReceiveMoney(float money){
            score = money;
        }
    }
}
