using CafeGame;
using UnityEngine;
using UnityEngine.UI;

public class CakeSelect : MonoBehaviour, ITexturable
{
    GANTexture texture;
    [SerializeField] RawImage textureSurface;
    [SerializeField] GANTextureSO cakeMakerChannel;
    [SerializeField] BoolEvent cakeMakerUIChannel;

    public GANTexture GetTexture()
    {
        return texture;
    }

    public void SetTexture(GANTexture ganTexture)
    {
        texture = new GANTexture(ganTexture);
        textureSurface.texture = texture.Texture;
    }

    public void Make(){
        cakeMakerChannel.RaiseEvent(texture, true);
    }

    public void CloseUI(){
        cakeMakerUIChannel.RaiseEvent(false);
    }

}
