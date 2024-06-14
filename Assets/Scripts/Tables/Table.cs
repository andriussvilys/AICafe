using UnityEngine;
using System;

namespace CafeGame{
    public class Table : Interactable
    {
        [SerializeField] Seat seat;

        [SerializeField] GameObject foodPrefab;
        
        public Food cake;
        public Food Cake{get{return cake;}}

        NPC npc;
        public NPC Customer {get {return npc;}}

        public void SetCustomer(NPC customer){
            this.npc = customer;
            seat.Occupy(customer);
            customer.transform.SetParent(transform);
        }

        public bool IsFree(){
            return cake == null && !seat.Occupied;
        }

        internal void FreeUp()
        {
            seat.FreeUp();
        }

        protected override void Start() {
            base.Start();
            SetMessage(MESSAGES.FOOD_PUTDOWN);
        }

        public override bool Interact(Player player)
        {
            if(cake == null){
                GANTexture cakeTexture = player.Inventory.Take();
                GameObject instance = Instantiate(foodPrefab, new Vector3(), new Quaternion(), transform);
                // instance.transform.localPosition = new Vector3();
                instance.GetComponent<Food>().SetTexture(cakeTexture);
                instance.transform.localScale = new Vector3(1,1,1);
                instance.transform.SetLocalPositionAndRotation(new Vector3(0,-0.5f,0), new Quaternion());
                cake = instance.GetComponent<Food>();
                cake.GetComponent<Collider>().enabled = false;
                if(npc != null){
                    npc.ReceiveCake(cake);
                }
                SetMessage(MESSAGES.FOOD_PICKUP);
            }
            else{
                SetMessage(MESSAGES.FOOD_PUTDOWN);
                cake.Interact(null);
                cake = null;
            }
            return true;
        }


    }

}
