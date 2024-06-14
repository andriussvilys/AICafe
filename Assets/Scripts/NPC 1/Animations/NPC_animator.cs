using UnityEngine;

public class NPC_animator : MonoBehaviour
{
    [SerializeField]
     private Animator _animator;
     public Animator Animator {get {return _animator;} set {}}

     int idle, walk, run, sit;

     public void Initialize()
     {
         _animator = GetComponent<Animator>();
         idle = Animator.StringToHash(AnimationNames.idle);
         walk = Animator.StringToHash(AnimationNames.walk);
         run = Animator.StringToHash(AnimationNames.run);
         sit = Animator.StringToHash(AnimationNames.sit);
     }

     void Awake()
     {
        Initialize();
     }

     public void SetState( string stateName, bool value){
        switch(stateName){

            case AnimationNames.idle:
                _animator.SetBool(idle, value);
                break;

            case AnimationNames.walk:
                // _animator.SetBool(walk, value);
                _animator.Play(walk, -1, 1);
                break;

            case AnimationNames.run:
                _animator.SetBool(run, value);
                break;
            case AnimationNames.sit:
                _animator.SetBool(sit, value);
                break;

            default:
                break;

        }
     }
}
