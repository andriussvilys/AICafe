using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractionMessageDisplay : MonoBehaviour
{
    [SerializeField]
    BoolStringEvent_SO interactionMessageChannel;
    Image image;

    private void Start() {
        image = GetComponentInChildren<Image>();
        ToggleBackground(0);
    }

    private void OnEnable() {
        interactionMessageChannel.OnEventRaise += ChangeText;
    }
    private void OnDisable() {
        interactionMessageChannel.OnEventRaise -= ChangeText;
    }

    private void ChangeText(bool value, string message){
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;
        ToggleBackground(value ? 1 : 0);
    }

    private void ToggleBackground(float value){
        Color imageColor = image.color;
        imageColor.a = value;
        image.color = imageColor;
    }
}
