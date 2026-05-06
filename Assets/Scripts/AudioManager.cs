using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

public Sound[] sounds;

    // Initialisation for the sounds
    void Awake() {
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioScource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void Play (string name) {
        
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();


    }
}
