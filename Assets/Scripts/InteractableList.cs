using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableList<T> : MonoBehaviour
{
    [SerializeField]
    GOEventSO interactionChannel;
    public List<T> list;

    void Start()
    {
        list = new List<T>(InstantiateList());
    }

    private void OnEnable() {
        interactionChannel.OnEventRaise += ModifyList;
    }

    private void OnDisable() {
        interactionChannel.OnEventRaise -= ModifyList;
    }

    private void ModifyList(GameObject obj, bool value){
        if(value){
            OnAdd(obj);
        }
        else{
            OnRemove(obj);
        }
    }

    protected abstract void OnAdd(GameObject GO);
    protected abstract void OnRemove(GameObject GO);

    protected abstract List<T> InstantiateList();
}
