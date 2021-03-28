using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    public Sounds[] Sounds;
    GameManager manager;
    public bool SoundMuted;

    //Timers
    private float timePlayingHomeBackground;
    private float timePlayingHunerGrowl = 60;


    void Start ()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();

        //Create Audiosources for all Sounds
        foreach (Sounds SoundClip in Sounds)
        {
            SoundClip.source = gameObject.AddComponent<AudioSource>();
            
            SoundClip.source.clip = SoundClip.clip;

            SoundClip.source.volume = SoundClip.volume;
            SoundClip.source.pitch = SoundClip.pitch;

        }
    }

    private void Update()
    {
        //timer that replays the background music in MonsterHome
        timePlayingHomeBackground += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            //unmutes the monster while its in the MonsterHome scene
            if (manager.ActiveMonster.SetMute)
                manager.ActiveMonster.SetMute = false;
            //when the background music is finished playing, replay it.
            if (timePlayingHomeBackground >= Sounds[5].clip.length)
            {
                FindObjectOfType<SoundManager>().play("BackgroundMusic");
                timePlayingHomeBackground = 0;
            }
            //play the HungerGrowl sound effect if its hunger is under 25, ever 60 sec.
            if (manager.ActiveMonster.HungerStatus <= 25)
            {
                timePlayingHunerGrowl += Time.deltaTime;
                if (manager.ActiveMonster != null && timePlayingHunerGrowl >= 60)
                {
                    play("HungerGrowl");
                    timePlayingHunerGrowl = 0;
                }
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (manager != null)
        {
            if (SceneManager.GetActiveScene().name == "MonsterHome")
            {
                FindSource("DesertBattleMusic").Stop();
                FindSource("ForestBattleMusic").Stop();
                FindSource("SavannaBattleMusic").Stop();
                play("BackgroundMusic");

            }
            //Music for the different zones        
            //Desert
            if (SceneManager.GetActiveScene().name == "Desert_FS")
            {
                FindSource("BackgroundMusic").Stop();
                FindObjectOfType<SoundManager>().play("DesertBattleMusic");
            }
            //Forest
            if (SceneManager.GetActiveScene().name == "Forest_FS")
            {
                FindSource("BackgroundMusic").Stop();
                FindObjectOfType<SoundManager>().play("ForestBattleMusic");
            }
            //Savanna
            if (SceneManager.GetActiveScene().name == "Savannah_FS")
            {
                FindSource("BackgroundMusic").Stop();
                FindObjectOfType<SoundManager>().play("SavannaBattleMusic");
            }
        }
    }


    /// <summary>
    /// Play a sound clip.
    /// </summary>
    /// <param name="name"></param>
    public void play(string name)
    {
        Sounds s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Play();
    }


    /// <summary>
    /// Find an AudioSource attached to the SoundManager.
    /// </summary>
    /// <param name="ClipName"></param>
    /// <returns></returns>
    public AudioSource FindSource(string ClipName) 
    {
        AudioSource[] Sources = gameObject.GetComponents<AudioSource>();
        foreach (AudioSource Source in Sources)
        {
            if (Source.clip.name == ClipName)
            {
                return Source;
            }
        }
        Debug.LogWarning("AudioSource Not Found");
        return null;
    }
}
