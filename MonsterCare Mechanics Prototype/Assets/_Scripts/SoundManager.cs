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
    private float timePlayingSnoring = 60;


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

        //Sets the current muted value.
        SetMute(SoundMuted);
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
                       //Snoring
            if (manager.ActiveMonster.IsSleepingStatus == true)
            {
                timePlayingSnoring += Time.deltaTime;
                if (manager.ActiveMonster != null && timePlayingSnoring >= 60)
                {
                    play("Snoring");
                    timePlayingSnoring = 0;
                }
            }
        }

        //Obtained Report
        /*if (isCreated == true)
        {
            play("ObtainReport");
        }       */

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
                FindSource("MountainBattleMusic").Stop();
                FindSource("IceBattleMusic").Stop();
                play("BackgroundMusic");

            }
            //Music for the different zones        
            //Desert
            if (SceneManager.GetActiveScene().name == "Desert_FS")
            {
                FindSource("BackgroundMusic").Stop();
                play("DesertBattleMusic");
            }
            //Forest
            if (SceneManager.GetActiveScene().name == "Forest_FS")
            {
                FindSource("BackgroundMusic").Stop();
                play("ForestBattleMusic");
            }
            //Savanna
            if (SceneManager.GetActiveScene().name == "Savannah_FS")
            {
                FindSource("BackgroundMusic").Stop();
                play("SavannaBattleMusic");
            }
            //Ice
            if (SceneManager.GetActiveScene().name == "Ice_FS")
            {
                FindSource("BackgroundMusic").Stop();
                play("IceBattleMusic");
            }

            //Mountain
            if (SceneManager.GetActiveScene().name == "Mountain_FS")
            {
               FindSource("BackgroundMusic").Stop();
               play("MountainBattleMusic");
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

    //Updates every audiosource to mute/unmuted
    public void SetMute(bool Mute) 
    {
        SoundMuted = Mute;
        if (SoundMuted)
        {
            //mutes all audiosources
            foreach (AudioSource sounds in GetComponents<AudioSource>())
            {
                sounds.mute = true;
            }
        }
        else
        {
            //unmutes all audiosources
            foreach (AudioSource sounds in GetComponents<AudioSource>())
            {
                sounds.mute = false;
            }
        }
    }
}
