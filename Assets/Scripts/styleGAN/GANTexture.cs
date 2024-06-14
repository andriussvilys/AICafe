using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CafeGame{
    public class GANTexture
    {
        Texture2D texture;
        float[] styleVector;
        public Texture2D Texture { get => texture; set => texture = value; }
        public float[] StyleVector { get => styleVector; set {styleVector = new float[value.Length]; value.CopyTo(styleVector, 0);} }

        public GANTexture(Texture2D texture, float[] styleVector){
            Texture = texture;
            StyleVector = styleVector;
        }

        public GANTexture(){
            Texture = null;
            StyleVector = new float[0];
        }

        public GANTexture(GANTexture ganTexture){
            Texture = ganTexture.Texture;
            StyleVector = ganTexture.StyleVector;
        }
    }
}
