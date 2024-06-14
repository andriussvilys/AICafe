using UnityEngine;
using UnityEngine.Events;
using CafeGame;

[CreateAssetMenu(menuName = "Events/GANTexture Event")]
public class GANTextureSO : ScriptableObject
{
    public UnityAction<GANTexture, bool> OnEventRaise;

    public void RaiseEvent(GANTexture texture, bool value){
        OnEventRaise?.Invoke(texture, value);
    }

}