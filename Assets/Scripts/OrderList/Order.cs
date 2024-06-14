using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace CafeGame{
    public class Order
    {
        GANTexture customer;
        GANTexture cake;
        float satisfaction;

        public GANTexture Customer { get => customer; }
        public GANTexture Cake { get => cake; }
        public float Satisfaction { get => satisfaction; }

        public Order(GANTexture customer){
            this.customer = customer;
        }

        public Order(GANTexture customer, GANTexture cake){
            this.satisfaction = UTILS.CalcSimilarity(customer.StyleVector, cake.StyleVector);
            this.cake = cake;
            this.customer = customer;
        }

        public Order Update(GANTexture cake){
            this.cake = cake;
            this.satisfaction = UTILS.CalcSimilarity(customer.StyleVector, cake.StyleVector);
            return this;
        }
    }

}