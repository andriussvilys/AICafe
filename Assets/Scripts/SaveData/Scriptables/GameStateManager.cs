using UnityEngine;
using CI.QuickSave;
using System;
using SaveSystem;

[CreateAssetMenu (menuName = "misc/GameStateManager")]
public class GameStateManager: ScriptableObject
{
    QuickSaveReader saveReader;
    // private SaveSystem.SaveState _state = null;
    private SaveSystem.SaveState _state = new SaveState();
    bool savedGameExists = true;
    public bool SavedGameExists {get{return savedGameExists;}}

    private void OnEnable() {
        Init();
    }
    public void Init(){
        Debug.Log("init state");
        try{
            saveReader = QuickSaveReader.Create("saveFile");
            _state = saveReader.Read<SaveSystem.SaveState>("gamestate");
        }
        catch(Exception error){
            savedGameExists = false;
            Debug.Log(error);
        }
    }

    public SaveSystem.SaveState GetState(){
        return _state;
    }

    public void SetState(SaveSystem.StateName name, object saveData){
        switch (name)
        {
            case SaveSystem.StateName.Player:
                _state.player = saveData as Player;
                break;
            default: break;
        }
    }

    public void ReloadState(){
        _state = new SaveSystem.SaveState();
    }

    public void Save(){
        var writer = QuickSaveWriter.Create("saveFile");
        writer.Write<SaveSystem.SaveState>("gamestate", _state);
        writer.Commit();
        if(saveReader != null){
            saveReader.Reload();
        }
        savedGameExists = true;
    }
}
