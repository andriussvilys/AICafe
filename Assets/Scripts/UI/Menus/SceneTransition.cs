
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    private Tween _fadeInTween;
    private Tween _fadeOutTween;

    public Tween FadeOut(float duration = 0.3f){
        return _image.GetComponent<Graphic>().DOFade(0, duration);
    }

    public Tween FadeIn(float duration = 0.3f){
        return _image.GetComponent<Graphic>().DOFade(1, duration);
    }

    private void PrepareTransition(){
        if(!_image.gameObject.activeSelf){
            _image.gameObject.SetActive(true);
        }
    }

    public IEnumerator FadeInScene(float duration, Action callback){
            PrepareTransition();
        if(_fadeOutTween != null){
            yield return _fadeOutTween.WaitForCompletion();
            _fadeOutTween = null;
            Tween temp = FadeIn(duration);
            _fadeInTween = temp;
            yield return _fadeInTween.WaitForCompletion();
            _fadeInTween = null;
            callback();
        }
        else{
            Tween temp = FadeIn(duration);
            _fadeInTween = temp;
            yield return _fadeInTween.WaitForCompletion();
            callback();
        }
    }

    public IEnumerator FadeOutScene(float duration, Action callback){
            PrepareTransition();
        if(_fadeInTween != null){
            yield return _fadeInTween.WaitForCompletion();
            _fadeInTween = null;
            Tween temp = FadeOut(duration);
            _fadeOutTween = temp;
            yield return _fadeOutTween.WaitForCompletion();
            _fadeOutTween = null;
            callback();
        }
        else{
            Tween temp = FadeOut(duration);
            _fadeOutTween = temp;
            yield return _fadeOutTween.WaitForCompletion();
            callback();
        }
    }
}