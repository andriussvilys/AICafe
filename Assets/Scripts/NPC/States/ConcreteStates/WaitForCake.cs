
using System.Collections.Generic;
using System.Diagnostics;

namespace CafeGame{
    public class WaitForCake : NPCState
    {
        public WaitForCake(NPCStateContext context, float duration) : base(context, duration) {
            name = NPC_STATE_NAME.WAIT_FOR_FOOD;
            npc = context.npc;
            message = "Is my cake on the way?";
        }

        public override bool Handle(Player player)
        {
            if(player == null){
                context.Switch();
            }
            return true;
        }

        protected override void OnEnter()
        {
            NPC npc = context.npc;
            npc.seatingChannel.RaiseEvent(npc.gameObject, true);
            npc.Animator.Switch(AnimationNames.sit);
        }

        protected override void OnExit()
        {
        }
    }
}
