using UnityEngine;

[RequireComponent(typeof(NPC_animator))]
public class AnimationSwitch : MonoBehaviour
{
    NPC_animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<NPC_animator>();
    }
    
    string prevAnimationName;

    private void Start() {
        animator.SetState(AnimationNames.idle, true);
        prevAnimationName = AnimationNames.idle;
    }

    public void Switch(string animationName){
        animator.SetState(prevAnimationName, false);
        animator.SetState(animationName, true);
        prevAnimationName = animationName;
    }

}
