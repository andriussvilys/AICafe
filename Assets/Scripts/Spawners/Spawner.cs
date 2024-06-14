using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using CafeGame;
using System.Linq;

public abstract class Spawner<T> : Persistable
{
    [SerializeField] Key inputKey;
    [SerializeField] protected GameObject instancePrefab;
    [SerializeField ] protected GanLoader ganLoader;

    private async void Update() {
        if(Keyboard.current[inputKey].wasPressedThisFrame){
            await CreateRandomInstance(new float[0]);
        }
    }

    protected async Task<GameObject> CreateRandomInstance(float[] input){
        float[] styleVector = UTILS.GetRandomStyleVector(512);
        return await CreateInstance(styleVector);
    } 

    protected async Task<GameObject> CreateInstance(float[] styleVector){
        GANTexture texture = await ganLoader.GetTextureAsync(styleVector);

        GameObject instance = Instantiate(instancePrefab);
        ITexturable texturable = instance.GetComponent<ITexturable>();
        texturable.SetTexture(texture);
        instance.transform.SetParent(gameObject.transform);

        return instance;
    }

    protected abstract Task UpdateSpawnerState(SaveSystem.SaveState state);

    protected abstract Task<bool> LoadSpawnerState(SaveSystem.SaveState saveState);

    protected List<T> GetChildren(){
        List<T> children = GetComponentsInChildren<T>().ToList();
        return children;
    }

    protected override Task UpdateState()
    {
        Debug.Log($"update spawner {gameObject.name}");
        return UpdateSpawnerState(StateManager.GetState());
    }

    protected override Task<bool> LoadState(SaveSystem.SaveState saveState)
    {
        return LoadSpawnerState(saveState);
    }
}
