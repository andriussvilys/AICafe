using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Readable : MonoBehaviour{

    [SerializeField]
    public BoolStringEvent_SO interactionMessageChannel;

    [SerializeField]
    public string message = "";
    public string Message {get{return message;} set{message = value;}}

    public void ShowMessage(){
        interactionMessageChannel.RaiseEvent(true, message);
    }

    public void HideMessage(){
        interactionMessageChannel.RaiseEvent(false, "");
    }

}
