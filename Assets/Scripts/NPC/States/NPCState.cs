using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafeGame
{    
    public abstract class NPCState
    {
        public NPC_STATE_NAME name;
        public string message;
        protected NPCStateContext context;
        protected float duration;
        public float Duration {get {return duration;}}
        float elapsedTime;
        public float ElapsedTime {get {return elapsedTime;}}
        protected NPC npc;
        public NPCState(NPCStateContext context, float duration){
            this.duration = duration; 
            this.context = context;
            npc = context.npc;
        }
        public abstract bool Handle(Player player);

        public void Enter(){
            Debug.Log($"enter, duration: {duration}");
            context.npc.ShowVFX();
            SetReadableMessage();
            context.spinner.ChangeColor(Color.green);
            context.spinner.Run(duration);
            OnEnter();
        }
        protected abstract void OnEnter();

        private void SetReadableMessage(){
            context.npc.GetComponent<Interactable>().SetMessage(message);
        }

        public  void Exit(){
            OnExit();
        }
        protected abstract void OnExit();

        public virtual void Update(float increment){
            elapsedTime += increment;
            if(elapsedTime > duration){
                context.spinner.ChangeColor(Color.red);
            }
        }

    }
}
