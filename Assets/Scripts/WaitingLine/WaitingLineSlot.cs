using UnityEngine;

public class WaitingLineSlot : MonoBehaviour
{
    GameObject customer;
    public GameObject Customer {get{return customer;}}
    public void Add(GameObject GO){
        customer = GO;
        GO.transform.position = gameObject.transform.position;
        GO.transform.rotation = gameObject.transform.rotation;
        GO.transform.SetParent(transform);
    }
    public void Remove(){
        customer = null;
    }

    private void OnDestroy() {
        Remove();
    }
}
