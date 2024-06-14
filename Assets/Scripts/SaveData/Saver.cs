using System.Collections;
using UnityEngine;

public class Saver : MonoBehaviour {

    [SerializeField] GameStateManager StateManager;

    [SerializeField] SubscriberEvent saveEventChannel;

    [SerializeField] PauseMenu pauseMenu;

    bool _saving = false;

    public async void Save(){
        if(_saving)
            return;

        _saving = true;
        await saveEventChannel.RaiseEvent();
        StateManager.Save();
        pauseMenu.Unpause();
        _saving = false;
    }
    
}


