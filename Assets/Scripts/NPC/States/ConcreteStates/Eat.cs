using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafeGame{
    public class Eat : NPCState
    {
        public Eat(NPCStateContext context, float duration) : base(context, duration) {
            name = NPC_STATE_NAME.EAT;
            message = "om nom.. om nom..";
        }

        public override bool Handle(Player player)
        {
            return true;
        }

        protected override void OnEnter()
        {
            context.npc.Cake.ShowTimer(duration);
        }

        protected override void OnExit()
        {
        }

        public override void Update(float increment){
            base.Update(increment);
            if(ElapsedTime > Duration){
                context.Switch();
            }
        }
    }
}
