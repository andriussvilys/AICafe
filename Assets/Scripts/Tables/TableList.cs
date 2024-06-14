using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CafeGame;

public class TableList : InteractableList<Table>
{
    protected override List<Table> InstantiateList()
    {
        Table[] seat_arr = FindObjectsByType<Table>(FindObjectsSortMode.None);
        return new List<Table>(seat_arr);
    }

    protected override void OnAdd(GameObject GO)
    {
        AssignSeat(GO);
    }

    protected override void OnRemove(GameObject GO)
    {
        FreeUpSeat(GO);
    }

    private void AssignSeat(GameObject GO){

        CafeGame.NPC customer = GO.GetComponent<CafeGame.NPC>();
        List<Table> freeTables = list.FindAll(table => table.IsFree());
        int randomIndex = Random.Range(0, freeTables.Count);
        Table randomTable = freeTables[randomIndex];
        randomTable.SetCustomer(customer);
    }

    private void FreeUpSeat(GameObject GO){
        CafeGame.NPC customer = GO.GetComponent<CafeGame.NPC>();
        Table occupiedTable = list.Find(table => ReferenceEquals(table.Customer, customer));
        occupiedTable.FreeUp();
    }

}