using System.Collections.Generic;
using System.Linq;
using CafeGame;
using UnityEngine;

public class CakeMakerUI : Menu
{
    [SerializeField] BoolEvent screenMakerUIChannel;
    [SerializeField] BoolEvent cakeGANLoadedChannel;
    [SerializeField] GanLoader cakeGAN;
    [SerializeField] GameObject cakeSelectPrefab;
    [SerializeField] Transform cakeSelectContainer;

    private async void LoadTextures(bool value){
        double[][] latents = LoadLatents.LoadLatentsFromJson();
        float[][] latentsFloat = new float[latents.Length][];
        latentsFloat = latents.Select(latent => latent.Select(d => (float)d).ToArray()).ToArray();
        int[] randomIndices = new int[4];
        for (int i = 0; i < randomIndices.Length; i++)
        {
            randomIndices[i] = UnityEngine.Random.Range(0, latentsFloat.Length);
        }
        for (int i = 0; i < randomIndices.Length; i++)
        {
            GANTexture texture = await cakeGAN.GetTextureAsync(latentsFloat[randomIndices[i]]);
            GameObject instance = Instantiate(cakeSelectPrefab, new Vector2(), new Quaternion(), cakeSelectContainer);
            instance.GetComponent<ITexturable>().SetTexture(texture);
        }
    }

    private void OnEnable() {
        screenMakerUIChannel.Subscribe(Activate);
        cakeGANLoadedChannel.Subscribe(LoadTextures);
    }
    private void OnDisable() {
        screenMakerUIChannel.Unsubscribe(Activate);
        cakeGANLoadedChannel.Unsubscribe(LoadTextures);
    }

    private void Activate(bool value){
        if(value){
            Enter(null);
        }
        else{
            Leave(null);
        }
    }

    public void Close(){
        Activate(false);
    }

}
