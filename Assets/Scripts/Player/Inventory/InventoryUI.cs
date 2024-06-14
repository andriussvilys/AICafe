using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using CafeGame;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GANTextureListSO foodTextureListChannel;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] GameObject container;

    private void OnEnable() {
        foodTextureListChannel.OnEventRaise += Render;
    }

    private void OnDisable() {
        foodTextureListChannel.OnEventRaise -= Render;
    }

    private void Render(List<GANTexture> foodList)
    {
        ClearContainer();
        foreach(GANTexture ganTexture in foodList){
            GameObject slotInstance = Instantiate(slotPrefab);
            Texture2D meshTexture = ganTexture?.Texture;
            RawImage slotImage = slotInstance.GetComponent<RawImage>();
            slotImage.texture = meshTexture;
            slotImage.color = new Color(1,1,1,1);
            slotInstance.transform.SetParent(container.transform);
        }
    }

    private void ClearContainer(){
        foreach(Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
