using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CafeGame{
    [CreateAssetMenu(menuName = "NPC/SoundListSO")]
    public class SoundListSO : ScriptableObject
    {
        [SerializeField]
        string _name;
        [SerializeField]
        List<AudioClip> _audioClips;

        public string Name {get {return _name;}}

        public int Count {get {return _audioClips.Count;}}

        public AudioClip Get(int index){
            return _audioClips[index];
        }

    }
}
