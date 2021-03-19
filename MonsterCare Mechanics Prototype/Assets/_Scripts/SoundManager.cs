using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    public Sounds[] Sounds;

    void Start ()
    {
        foreach (Sounds SoundClip in Sounds)
        {
            SoundClip.source = gameObject.AddComponent<AudioSource>();
            
            SoundClip.source.clip = SoundClip.clip;

            SoundClip.source.volume = SoundClip.volume;
            SoundClip.source.pitch = SoundClip.pitch;

        }
    }

    public void play (string name)
    {
       Sounds s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Play();
    }

  


}
