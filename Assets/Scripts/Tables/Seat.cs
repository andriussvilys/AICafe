using UnityEngine;

public class Seat : MonoBehaviour
{
    [SerializeField] GameObject seat;

    bool occupied = false;
    public bool Occupied {get {return occupied;}}

    public void Occupy(CafeGame.NPC customer){
        // customer = GO.GetComponent<NPC.NPC>();
        occupied = true;
        customer.transform.rotation = seat.transform.rotation;
        transform.Translate(Vector3.forward * -0.3f, Space.Self);
        customer.transform.position = seat.transform.position;
        customer.transform.Translate(new Vector3(0.0f, -0.5f, -0.2f), Space.Self);
    }

    public void FreeUp(){
        occupied = false;
        transform.Translate(Vector3.forward * 0.3f, Space.Self);
    }

}
