using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace CafeGame{
    public class TableSpawner : Spawner<Table>
    {
        [SerializeField] GameObject npcPrefab;
        [SerializeField] GameObject cakePrefab;
        [SerializeField] GanLoader cakeGAN;
        [SerializeField] GanLoader faceGAN;
        protected async override Task<bool> LoadSpawnerState(SaveSystem.SaveState saveState)
        {
            List<SaveSystem.Table> data = saveState.tables.list;
            List<CafeGame.Table> tables = GetTables();
            
            foreach (CafeGame.Table table in tables)
            {
                Debug.Log("Table position: " + table.transform.position);
            }

            foreach (SaveSystem.Table elem in data)
            {
                CafeGame.Table found = tables.Find(table => table.transform.position == elem.transform.position);

                if(found != null){

                    SaveSystem.NPC customerData = elem.customer;
                    SaveSystem.Food cakeData = elem.customer.cake;

                    if(!IsArrayNull(customerData.styleVector)){
                        GANTexture customerTexture = await faceGAN.GetTextureAsync(customerData.styleVector);
                        GameObject customerInstance = Instantiate(npcPrefab);
                        customerInstance.GetComponent<ITexturable>().SetTexture(customerTexture);
                        if(!IsArrayNull(customerData.styleVector)){
                            GANTexture cakeTexture = await cakeGAN.GetTextureAsync(customerData.styleVector);
                            GameObject cakeInstance = Instantiate(cakePrefab, new Vector3(), new Quaternion(), found.transform);
                            cakeInstance.transform.SetLocalPositionAndRotation(found.transform.position, new Quaternion());
                            cakeInstance.GetComponent<ITexturable>().SetTexture(cakeTexture);
                            customerInstance.GetComponent<CafeGame.NPC>().ReceiveCake(cakeInstance.GetComponent<Food>());
                        }
                        customerInstance.GetComponent<CafeGame.NPC>().SetState(customerData.activeState);
                    }
                }
                else{
                    Debug.Log("table not found");
                }

            }
            return true;
        }

        protected override Task UpdateSpawnerState(SaveSystem.SaveState state)
        {
            List<CafeGame.Table> tables = GetTables();
            List<CafeGame.Table> occupied = tables.FindAll(table => table.IsFree());
            List<SaveSystem.Table> list = occupied.Select(elem => new SaveSystem.Table(elem)).ToList();
            state.tables = new SaveSystem.Spawner<SaveSystem.Table>(list);
            return null;
        }

        private List<CafeGame.Table> GetTables(){
            List<CafeGame.Table> tables = FindObjectsByType<CafeGame.Table>(FindObjectsSortMode.None).ToList();
            return tables;
        }

        private bool IsArrayNull(float[] arr){
            bool empty = true;
            for (int i = 0; i < arr.Length; i++)
            {
                if(arr[i] != 0.0){
                    empty = false;
                    break;
                }
            }
            return empty;
        }
    }
}
    
