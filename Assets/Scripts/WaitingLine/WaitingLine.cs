using System.Collections.Generic;
using UnityEngine;

public class WaitingLine : InteractableList<WaitingLineSlot>
{
    [SerializeField]
    GameObject slotPrefab;

    protected override void OnAdd(GameObject GO)
    {
        AddSlot(GO);
    }

    protected override void OnRemove(GameObject GO)
    {
        RemoveSlot(GO);
    }

    protected override List<WaitingLineSlot> InstantiateList()
    {
        return new List<WaitingLineSlot>();
    }

    private void AddSlot(GameObject GO){
        GameObject slotInstance = Instantiate(slotPrefab, transform);
        WaitingLineSlot slot = slotInstance.GetComponent<WaitingLineSlot>();
        slot.transform.position = transform.position + Vector3.forward * (-2.0f * list.Count - 1.0f);
        list.Add(slot);
        slot.Add(GO);
    }

    private void RemoveSlot(GameObject GO){
        WaitingLineSlot occupiedSlot = list.Find(slot => ReferenceEquals(GO, slot.Customer));
        int index = list.FindIndex(slot => ReferenceEquals(slot, occupiedSlot));
        if(index > -1){
            MoveLine(index);
        }
        occupiedSlot.Customer.transform.SetParent(transform);
        list.Remove(occupiedSlot);
        Destroy(occupiedSlot.gameObject);
    }

    private void MoveLine(int index){
        for (int i = index; i < list.Count; i++)
        {
            WaitingLineSlot slot = list[i];
            Vector3 position = slot.transform.position;
            Vector3 newPosition = position + Vector3.forward * 2.0f;
            slot.transform.position = newPosition;
            slot.Customer.transform.position = newPosition;
        }
    }
}
