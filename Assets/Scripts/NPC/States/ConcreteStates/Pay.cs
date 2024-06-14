using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafeGame{
    public class Pay : NPCState
    {
        public Pay(NPCStateContext context, float duration) : base(context, duration)
        {
            name = NPC_STATE_NAME.READY_TO_PAY;
            message = "I'm ready to pay";
        }

        public override bool Handle(Player player)
        {
            context.npc.orderChannel.RaiseEvent(new CafeGame.Order(npc.GetTexture(), npc.Cake.GetTexture()));
            npc.ShowVFX();
            npc.DestroySelf();
            return true;
        }

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }
    }
}
