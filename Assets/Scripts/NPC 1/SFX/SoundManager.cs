using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CafeGame;
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    protected AudioSource _audioSource;

    [SerializeField]
    protected List<CafeGame.SoundListSO> _audioClipsDictionary;

    protected bool _soundIsPlaying = false;
    protected float _timeElapsed = 0;
    protected float _currentClipLength = 0;

    public void PlaySound(string name){

        CafeGame.SoundListSO soundList = _audioClipsDictionary.Find( soundList => {
            return soundList.Name == name;
        }
        );
        
        if(soundList){

            int randomIndex = Random.Range(0, soundList.Count-1);
            AudioClip audioClip = soundList.Get(randomIndex);
            _currentClipLength = audioClip.length;
            _audioSource.PlayOneShot(audioClip);

        }

    }

}
