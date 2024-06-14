using System.Threading.Tasks;
using SaveSystem;
using UnityEngine;

public abstract class Persistable: MonoBehaviour{
    [SerializeField] protected GameStateManager StateManager;

    [SerializeField] protected SubscriberEvent _saveEventChannel;
    
    // [SerializeField]  protected BoolEvent _saveCompleteChannel;

    [SerializeField] protected SubscriberEvent _loadEventChannel;
    
    [SerializeField]  protected BoolEvent _loadCompleteChannel;

    protected virtual void OnEnable()
    {
        _saveEventChannel.Subscribe(Save);
        _loadEventChannel.Subscribe(Load);
    }

    protected virtual void OnDisable()
    {
        _saveEventChannel.Unsubscribe(Save);
        _loadEventChannel.Unsubscribe(Load);
    }
    
    protected abstract Task UpdateState();

    private async Task Save(){
        Debug.Log($"save {gameObject.name}");
        UpdateState();
    }
    
    private async Task Load()
    {
        Debug.Log($"load {gameObject.name}");
      
        await LoadState(StateManager.GetState());
        Debug.Log($"load complete {gameObject.name}");
        // _loadCompleteChannel.RaiseEvent(true);
    }

    protected abstract Task<bool> LoadState(SaveState saveState);

}
