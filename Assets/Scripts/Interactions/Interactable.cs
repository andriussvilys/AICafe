using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using UnityEngine;

namespace CafeGame{
    [RequireComponent(typeof(Readable))]
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        Readable readable;

        protected virtual void Start() {
            readable = GetComponent<Readable>();
        }
        public abstract bool Interact(Player player);

        public void SetMessage(string message){
            readable.message = message;
        }

    }
}
