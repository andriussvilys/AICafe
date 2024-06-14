using System.Collections.Generic;
using UnityEngine;

namespace CafeGame{
    public class Door : Interactable
    {
        public Animator openandclose;

        bool open = false;

        protected override void Start() {
            base.Start();
            SetMessage(MESSAGES.DOOR_OPEN);
        }

        public void Opening()
		{
			openandclose.Play("Opening");
		}

		public void Closing()
		{
			openandclose.Play("Closing");
		}

        public override bool Interact(Player player)
        {
            if(!open){
                Opening();
                SetMessage(MESSAGES.DOOR_CLOSE);
            }
            else{
                Closing();
                SetMessage(MESSAGES.DOOR_OPEN);
            }
            open = !open;
            return true;
        }
    }
}
