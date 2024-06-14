using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CafeGame{
    public class WaitInLine : NPCState
    {
        public WaitInLine(NPCStateContext context, float duration) : base(context, duration) {
            name = NPC_STATE_NAME.WAIT_IN_LINE;
            message = "I'm ready to order";
            npc = context.npc;
        }

        public override bool Handle(Player player)
        {
            npc.orderChannel.RaiseEvent(new Order(npc.GetTexture()));
            context.Switch();
            return true;
        }

        protected override void OnEnter()
        {
            npc.waitingLineChannel.RaiseEvent(npc.gameObject, true);
        }

        protected override void OnExit()
        {
            npc.waitingLineChannel.RaiseEvent(npc.gameObject, false);
        }
    }
}
