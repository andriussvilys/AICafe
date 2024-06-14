using UnityEngine;
using CafeGame;

public class CakeScreen : CafeGame.Interactable
{
    [SerializeField] BoolEvent cakeMakerUIChannel;
    [SerializeField] CakeMaker cakeMaker;

    protected override void Start()
    {
        base.Start();
    }
    private void Update() {
        if(cakeMaker.Busy){
            SetMessage("CakeMaker is busy!");
        }
        else{
            SetMessage("Make a cake");
        }
    }
    public override bool Interact(Player player)
    {
        if(!cakeMaker.Busy){
            cakeMakerUIChannel.RaiseEvent(true);
        }
       return true;
    }
}
