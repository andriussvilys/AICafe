using System.Collections;
using UnityEngine;

public class CakeMaker : MonoBehaviour
{
    [SerializeField] GameObject VFX;
    [SerializeField] GameObject prefab;
    [SerializeField] UnityEngine.Transform instanceLocation;
    [SerializeField] Spinner spinner;
    [SerializeField] GANTextureSO cakeMakerChannel;
    bool busy = false;
    public bool Busy {get{return busy;} set{busy = value;}}

    private void OnEnable() {
        cakeMakerChannel.OnEventRaise += MakeCake;
    }
    private void OnDisable(){
        cakeMakerChannel.OnEventRaise -= MakeCake;
    }

    public void MakeCake(CafeGame.GANTexture texture, bool value){
        busy = true;
        GameObject VFXInstance = Instantiate(VFX, new Vector3(), new Quaternion(), transform);
        VFXInstance.transform.SetLocalPositionAndRotation(new Vector3(), new Quaternion());
        float randomValue = Random.Range(2, 5);
        spinner.Reset();
        spinner.ChangeColor(Color.red);
        spinner.Run(randomValue);
        StartCoroutine(FinishMakeCake(texture, VFXInstance));
    }

    IEnumerator FinishMakeCake(CafeGame.GANTexture texture, GameObject VFX){
        yield return new WaitUntil(() => spinner.IsFilled());
        spinner.ChangeColor(Color.green);
        GameObject instance = Instantiate(prefab);
        instance.GetComponent<CafeGame.Food>().SetTexture(texture);
        instance.transform.SetParent(instanceLocation);
        instance.transform.SetLocalPositionAndRotation(new Vector3(), new Quaternion());
        Destroy(VFX);
        busy = false;
    }
}
