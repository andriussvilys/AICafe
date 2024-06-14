using System;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour {

    [SerializeField ]private Image imageComp;
    public float speed = 200f;
    private float elapsedTime = 0.0f;
    private float duration = 0;
    public float Duration {get {return duration;}}
    public float ElapsedTime {get {return elapsedTime;}}
    private bool autoMode = false;

    private void Update() {
        if(autoMode){
            elapsedTime += Time.deltaTime;
            float fill = 1 - ((duration - elapsedTime)/duration);
            imageComp.fillAmount = fill;
            if(fill >= 1){
                autoMode = false;
            }
        }
    }

    public bool AddFill(float value){
        imageComp.fillAmount += value;
        return IsFilled();
    }
    public bool SetFill(float value){
        imageComp.fillAmount = value;
        return IsFilled();
    }
    public void ChangeColor(Color newColor){
        imageComp.color = newColor;
    }

    public void Reset(){
        imageComp.fillAmount = 0;
        duration = 0;
        elapsedTime = 0;
        autoMode = false;
    }

    public void Run(float duration){
        Reset();
        Debug.Log("run timer");
        this.duration = duration;
        autoMode = true;
    }

    public void SetSpeed(float speed){this.speed = speed;}

    internal bool IsFilled()
    {
        return imageComp.fillAmount >= 1f;
    }
}
