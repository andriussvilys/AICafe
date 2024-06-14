using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafeGame{
    public class Food : Interactable, ITexturable
    {
        [SerializeField]GANTextureSO textureChannel;

        [SerializeField] MeshRenderer textureSurface;
        [SerializeField] Spinner spinner;
        bool expired = false;
        public bool Expired {get {return expired;}}

        GANTexture ganTexture;

        protected override void Start() {
            base.Start();
            SetMessage(MESSAGES.FOOD_PICKUP);
        }

        private void PickUp(GANTexture texture){
            textureChannel.RaiseEvent(texture, true);
            Destroy(gameObject);
        }

        public void SetTexture(GANTexture ganTexture)
        {
            this.ganTexture = new GANTexture(ganTexture);
            UTILS.SetTexture(textureSurface, this.ganTexture.Texture);
        }

        public GANTexture GetTexture()
        {
            return ganTexture;
        }

        public override bool Interact(Player player)
        {
            PickUp(ganTexture);
            return true;
        }

        public void ShowTimer(float duration){
            spinner.Run((float)Math.Round(duration));
        }
    }
}
