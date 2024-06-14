using System.Collections;
using UnityEngine;

public class Loader : MonoBehaviour {

    [SerializeField] GameStateManager StateManager;

    [SerializeField] SubscriberEvent _loadEventChannel;

    [SerializeField] BoolEvent _loadCompleteChannel;

    [SerializeField] MainMenu mainMenu;


    bool _loading = false;

    int _responseCount = 0;

    void OnEnable(){_loadCompleteChannel.Subscribe(AcceptResponse);}

    void OnDisable(){ _loadCompleteChannel.Unsubscribe(AcceptResponse);}

    void AcceptResponse(bool value){
        if(_loading)
            _responseCount += 1;
    }

    public async void Load(){
        if(_loading)
            return;

        _loading = true;
        await _loadEventChannel.RaiseEvent();
        Debug.Log("all loaded");
        mainMenu.Close();
        _loading = false;
    }
    
}


