using UnityEngine;

public class PauseMenu : Menu
{
    private bool _isPaused = false;

    KeyCode pauseButton = KeyCode.P;  

    private void Update() {
        if (Input.GetKeyDown(pauseButton))
        {
            OnPauseTrigger();
        }
    }

    private void OnPauseTrigger(){
        if(!_isPaused){
            Pause();
        }
        else{
            Unpause();
        }
    }
    
    public void Unpause(){
        Leave(() => {
            _isPaused = false;
        });
    }

    public void Pause(){
        Enter(() => {
            _isPaused = true;
        });
    }

    public void NavigateToMainMenu(){
        base.NavigateToMainMenu(() => {
            _isPaused = false;
        });
    }

}
