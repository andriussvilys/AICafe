using UnityEngine;
using UnityEngine.UI;


public class MainMenu : Menu
{
    [SerializeField] GameStateManager stateManager;
    [SerializeField] Button _continueButton;

    // private void Start() {
    //     ActivateUI(true);
    // }

    protected void Start() {
        ShowMainMenu(true);
    }

    void OnEnable()
    {
        _showMainMenuChannel.Subscribe(ShowMainMenu);
    }
    
    void OnDisable()
    {
        _showMainMenuChannel.Unsubscribe(ShowMainMenu);
    }


    public void LaunchNewGame(){
        StartCoroutine(ChangeScreen(() => {
        }));
    }
    
    public void QuitGame(){
        Application.Quit();
    }

    private void ShowMainMenu(bool value){
        if(value){
            CheckIfSaved();
            Enter(() => {ActivateUI(true);});
        }
        else{
            Leave(() => {});
        }
    }

    public void Close(){
        Leave(() => {});
    }

    private void CheckIfSaved(){
        _continueButton.interactable = stateManager.SavedGameExists;
    }

}
