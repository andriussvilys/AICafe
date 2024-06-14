using System.Collections.Generic;
using UnityEngine;
using CafeGame;

public class Inventory : MonoBehaviour
{
    [SerializeField] GANTextureSO foodTextureChannel;
    [SerializeField] GANTextureListSO foodTextureListChannel;
    [SerializeField] GameObject foodPrefab;

    private List<GANTexture> foodList = new List<GANTexture>();
    public List<GANTexture> List{get {return foodList;} set {foodList = new List<GANTexture>(value);}}

    
    private void OnEnable() {
        foodTextureChannel.OnEventRaise += ModifyInventory;
    }

    private void OnDisable() {
        foodTextureChannel.OnEventRaise -= ModifyInventory;
    }

    public void ModifyInventory(GANTexture texture, bool value){
        if(value){
            GANTexture foodInstance = new GANTexture(texture); 
            foodList.Add(foodInstance);
        }
        foodTextureListChannel.RaiseEvent(foodList);
    }

    public GANTexture Take(){
        if(foodList.Count > 0){
            GANTexture texture = foodList[0];
            foodList.Remove(texture);
            foodTextureListChannel.RaiseEvent(foodList);
            return texture;
        }
        return null;
    }


}
