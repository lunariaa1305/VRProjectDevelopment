using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {

public Sound[] sounds;

    // Initialisation for the sounds
    void Awake() {
        foreach (Sounds s in sounds)
        {
           s.source = gameObject.Addcomponent<AudioScource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void Play (string name) {
        
        sounds s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();


    }
}
