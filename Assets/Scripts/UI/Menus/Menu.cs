using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    [SerializeField]
    protected BoolEvent _showMainMenuChannel;

    [SerializeField]
    protected SceneTransition _sceneTransition;

    protected float _fadeDuration = CONSTANTS.FADE_DURATION;

    protected CanvasGroup container;

    static protected Menu activeScreen;

    private void Awake() {
        container = GetComponent<CanvasGroup>();
        ActivateUI(false);
    }

    public void Enter(Action callback){
        if(activeScreen != null){
            return;
        }
        else{
            activeScreen = this;
            Time.timeScale = 1;
            StartCoroutine(_sceneTransition.FadeInScene(_fadeDuration, () => {
                ActivateUI(true);
                StartCoroutine(_sceneTransition.FadeOutScene(_fadeDuration, () => {
                    Time.timeScale = 0;
                    callback?.Invoke();
                }));
            }));
        }
    }

    public void Leave(Action callback){
        activeScreen = null;
        Time.timeScale = 1;
        StartCoroutine(_sceneTransition.FadeInScene(_fadeDuration, () => {
            ActivateUI(false);
            StartCoroutine(_sceneTransition.FadeOutScene(_fadeDuration, () => {
                activeScreen = null;
                callback?.Invoke();
            }));
        }));
    }

    public void ActivateUI(bool value){
        Debug.Log($"activate {gameObject.name} ui: {value}");
        container.alpha = value ? 1 : 0;
        container.blocksRaycasts = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value ? true : false;
    }

    public void NavigateToMainMenu(Action callback = null){
        StartCoroutine(ChangeScreen(
            () => {_showMainMenuChannel.RaiseEvent(true);},
            callback
        ));
    }

    protected IEnumerator ChangeScreen(Action sceneChangeAction, Action callback = null){
        activeScreen = null;
        Time.timeScale = 1;
        sceneChangeAction.Invoke();
        yield return new WaitForSeconds(_fadeDuration);
        ActivateUI(false);
        callback?.Invoke();
    }

}
