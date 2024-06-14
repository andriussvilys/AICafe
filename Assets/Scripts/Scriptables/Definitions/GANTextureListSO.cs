using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CafeGame;

[CreateAssetMenu(menuName = "Events/GANTextureList Event")]
public class GANTextureListSO : ScriptableObject
{
    public UnityAction<List<GANTexture>> OnEventRaise;

    public void RaiseEvent(List<GANTexture> list){
        OnEventRaise?.Invoke(list);
    }

}