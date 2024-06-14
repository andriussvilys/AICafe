using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SaveSystem
{
    [Serializable]
    public class Transform{
        public Vector3 position;
        public Quaternion rotation;
        public Transform(Vector3 position, Quaternion rotation){
            this.position = position;
            this.rotation = rotation;
        }
        public Transform(UnityEngine.Transform transform){
            // this.position = transform.position;
            this.position = new Vector3((float)Math.Round(transform.position.x, 2), (float)Math.Round(transform.position.y, 2), (float)Math.Round(transform.position.z, 2));
            this.rotation = transform.rotation;
        }
        public Transform(){
            position = new Vector3();
            rotation = new Quaternion();
        }
    }

    [Serializable]
    public class GANTexture{
        public float[] styleVector = new float[512];

        public GANTexture(float[] styleVector){
            this.styleVector = new float[512];
            styleVector.CopyTo(this.styleVector, 0);
        }
    }

    [Serializable]
    public class Order{
        public GANTexture customer;
        public GANTexture cake;
        public float satisfaction = 0f;

        // For loading
        public Order(){
            customer = new GANTexture(new float[512]);
            cake = new GANTexture(new float[512]);
        }

        // For saving
        public Order(CafeGame.Order order){
            customer = new GANTexture(order.Customer.StyleVector);
            if(order.Cake != null){
                cake = new GANTexture(order.Cake.StyleVector);
                satisfaction = UTILS.CalcSimilarity(this.customer.styleVector, this.cake.styleVector);
            }
            else{
                cake = null;
                satisfaction = 0f;
            }
        }
    }

    [Serializable]
    public class Player{
        public Transform transform;
        public float score;

        public List<GANTexture> inventory;
        public List<SaveSystem.Order> orders;

        public Player(){
            transform = new Transform(new Vector3(0,0,0), new Quaternion());
            score = 0f;
            inventory = new List<GANTexture>();
            orders = new List<Order>();
        }

        // For saving
        public Player(UnityEngine.Transform transform, float score, List<CafeGame.GANTexture> inventory, List<CafeGame.Order> orders){
            this.transform = new Transform(transform);
            this.score = score;
            this.inventory = inventory.Select(texture => new GANTexture(texture.StyleVector)).ToList();
            this.orders = orders.Select(order => new SaveSystem.Order(order)).ToList();
        }

    }

    [Serializable]
    public class NPC
    {
        public float[] styleVector;
        public Transform transform;
        public int activeState;
        public Food cake;

        public NPC(){
            styleVector = new float[512];
            transform = new SaveSystem.Transform();
            activeState = 0;
            cake = null;
        }

        public NPC(CafeGame.NPC customer)
        {
            if(customer != null){
                transform = new SaveSystem.Transform(customer.transform);
                styleVector = customer.GetTexture().StyleVector;
                cake = new SaveSystem.Food(customer.Cake);
                activeState = customer.GetActiveStateIndex();
            }
            else{
                styleVector = new float[512];
                transform = new SaveSystem.Transform();
                activeState = 0;
                cake = new Food();
            }
        }
    }

    [Serializable]
    public class Food
    {
        public float[] styleVector;
        public Transform transform;

        public Food(){
            styleVector = new float[512];
            transform = new SaveSystem.Transform();
        }

        public Food(CafeGame.Food food)
        {
            if(food != null){
                styleVector = food.GetTexture().StyleVector;
                this.transform = new Transform(food.transform);
            }
            else{
                styleVector = new float[512];
                transform = new SaveSystem.Transform();
            }
        }
    }

    [Serializable] public class Table{
        public Transform transform;
        // seat
        public NPC customer;
        public Food cake;

        public Table(){
            customer = new SaveSystem.NPC();
            cake = new SaveSystem.Food();
            transform = new SaveSystem.Transform();
        }

        public Table(CafeGame.Table table){
            this.customer = new SaveSystem.NPC(table.Customer);
            this.cake = new SaveSystem.Food(table.Cake);
            this.transform = new SaveSystem.Transform(table.gameObject.transform);
        }
    }

    [Serializable]
    public class Spawner<T>{
        public List<T> list;

        public Spawner(List<T> data){
            list = data;
        }
        public Spawner(){
            list = new List<T>();
        }
    }

    [Serializable]
    public class SaveMetaData{
        public DateTime saveTime = DateTime.Now;
        public int lastSavedSceneIndex = -1;
    }

    public enum StateName {Player, Tables, Customers, Food}

    [Serializable]
    public class SaveState
    {
        public Player player;
        public SaveMetaData metaData;
        public Spawner<NPC> waitingLine;
        public Spawner<Table> tables;
        public SaveState(){
            metaData = new SaveMetaData();
            player = new Player();
        }

    }


}
