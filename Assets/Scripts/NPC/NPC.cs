using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CafeGame
{
    public class NPC : Interactable, ITexturable
    {
        [SerializeField] MeshRenderer textureSurface;
        [SerializeField] GameObject VFX;
        [SerializeField] public GOEventSO waitingLineChannel;
        [SerializeField] public GOEventSO seatingChannel;
        [SerializeField] public OrderEventSO orderChannel;
        [SerializeField] Spinner spinnerPrefab;

        public MeshRenderer TextureSurface{get {return textureSurface; }}
        Spinner currentSpinner;

        AnimationSwitch animator;
        public AnimationSwitch Animator {get {return animator;}}

        GANTexture ganTexture;
        internal int stateIndex;

        public System.Guid Id { get; internal set; }

        List<NPCState> states;
        public List<NPCState> States{get {return states;}}

        NPCState currentState;
        public NPCState CurrentState{get {return currentState;}}

        public Food cake = null;
        public Food Cake {get{return cake;}}

        NPCStateContext stateManager;

        protected override void Start() {
            base.Start();
            animator = GetComponentInChildren<AnimationSwitch>();
            states = new List<NPCState>();
            currentSpinner = Instantiate(spinnerPrefab, new Vector3(0,0,0), new Quaternion(), textureSurface.transform);
            currentSpinner.transform.SetLocalPositionAndRotation(-Vector3.up, new Quaternion());
            stateManager = new NPCStateContext(this, currentSpinner);
            stateManager.Switch();
        }

        private void Update() {
            stateManager.Update(Time.deltaTime);
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

        public void ShowVFX(){
            Vector3 vfxPosition = transform.position + Vector3.up * 2.0f;
            GameObject vfx_enter = Instantiate(VFX, vfxPosition, new Quaternion());
            StartCoroutine(UTILS.DelayedDestroy(vfx_enter, 3.0f));
        }

        public void ReceiveCake(Food cake){
            if(stateManager.ActiveState.name == NPC_STATE_NAME.WAIT_FOR_FOOD){
                this.cake = cake;
                stateManager.Handle(null);
            }
        }

        public override bool Interact(Player player)
        {
            stateManager.Handle(player);
            return true;
        }

        public int GetActiveStateIndex(){
            return (int)stateManager.ActiveState.name;
        }

        public void SetState(int stateIndex){
            stateManager.SetState(stateIndex);
        }

        internal void DestroySelf()
        {
            Destroy(cake.gameObject);
            StartCoroutine(UTILS.DelayedDestroy(gameObject, 0.2f));
        }
    }
}
