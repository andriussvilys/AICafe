using System.Collections.Generic;

namespace CafeGame{
    public enum NPC_STATE_NAME {WAIT_IN_LINE, WAIT_FOR_FOOD, EAT, READY_TO_PAY}
    public class NPCStateContext
    {
        public Spinner spinner;
        public NPC npc;
        Queue<NPCState> history;
        NPCState activeState;
        public NPCState ActiveState{get{return activeState;}} 
        
        public NPCStateContext(NPC npc, Spinner spinner){
            this.spinner = spinner;
            this.npc = npc;
            InitStates();
        }

        private void InitStates(){
            float min = 10;
            float max = 20;
            history = new Queue<NPCState>();
            history.Enqueue(new WaitInLine(this, UnityEngine.Random.Range(min, max)));
            history.Enqueue(new WaitForCake(this, UnityEngine.Random.Range(min, max)));
            history.Enqueue(new Eat(this, UnityEngine.Random.Range(min, max)));
            history.Enqueue(new Pay(this, UnityEngine.Random.Range(min, max)));
        }

        public NPCState Switch(){
            if(history.Count == 0){
                return null;
            }
            if(activeState != null){
                activeState.Exit();
            }
            activeState = history.Dequeue();
            activeState.Enter();
            return activeState;
        }

        public void SetState(int stateIndex){
            InitStates();
            NPC_STATE_NAME state = (NPC_STATE_NAME)stateIndex;
            switch (stateIndex)
            {
                case 0:
                    break;
                case 1:
                    Switch();
                    break;
                case 2:
                    history.Dequeue();
                    Switch();
                    break;
                default: break;
            }
        }

        internal void Update(float deltaTime)
        {
            activeState.Update(deltaTime);
        }

        internal void Handle(Player player)
        {
            activeState.Handle(player);
        }
    }
}
