using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SpawnerLoader : Persistable
{

    [SerializeField]
    protected List<GameObject> _sceneList;

    protected List<GameObject> GetSceneList(){
        List<GameObject> newSceneList = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform list = transform.GetChild(i);
            for (int j = 0; j < list.childCount; j++)
            {
                newSceneList.Add(list.GetChild(j).gameObject);
            }
        }
        return newSceneList;
    }

    protected virtual void Start(){
        // int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneData sceneData = StateManager.GetScene(sceneIndex);
        // if(sceneData != null){
        //         _sceneList = GetSceneList();
        //         DestroyUnsaved(_sceneList, sceneData);
        //         LoadState(sceneIndex);
        // }
    }


    public void LoadState(int sceneIndex)
    {   
    }

    public void UpdateState(int sceneIndex)
    {
        // SceneData sceneData = StateManager.GetScene(sceneIndex);
        // if(sceneData == null){
        //     StateManager.SetScene(sceneIndex, new SceneData());
        // }
        // _sceneList = GetSceneList();
        // UpdateSpawnerState(sceneIndex);
        // _saveCompleteChannel.RaiseEvent(true);

    }

}
