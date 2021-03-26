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

    //Timers
    private float timePlayingHomeBackground;
    private float timePlayingHunerGrowl = 60;


    void Start ()
    {
        manager = GameObject.Find("__app").GetComponentInChildren<GameManager>();
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
        

        timePlayingHomeBackground += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "MonsterHome")
        {
            if (manager.ActiveMonster.SetMute)
                manager.ActiveMonster.SetMute = false;
            if (timePlayingHomeBackground >= Sounds[5].clip.length)
            {
                FindObjectOfType<SoundManager>().play("BackgroundMusic");
                timePlayingHomeBackground = 0;
            }


            if (manager.ActiveMonster.HungerStatus <= 25)
            {
                timePlayingHunerGrowl += Time.deltaTime;
                if (manager.ActiveMonster != null && timePlayingHunerGrowl >= 60)
                {
                    play("HungerGrowl");
                    Debug.LogError("playing growl");
                    timePlayingHunerGrowl = 0;
                }
                
            }
        }

    }

    private void OnLevelWasLoaded(int level)
    {
        if (manager != null)
        {
            Debug.Log("previous scene: " + manager.PreviousSecene);

            if (SceneManager.GetActiveScene().name == "MonsterHome")
            {

                //change to renderer so that stats can change while in other scenes?
                FindSource("DesertBattleMusic").Stop();
                FindSource("ForestBattleMusic").Stop();
                FindSource("SavannaBattleMusic").Stop();
                play("BackgroundMusic");

            }
            else
            {

            }

            if (Camera.main.GetComponent<AudioListener>() != null && manager.SoundMuted)
                Camera.main.GetComponent<AudioListener>().enabled = false;

            //Music for the different zones        //Desert
            if (SceneManager.GetActiveScene().name == "Desert_FS")
            {
                FindSource("BackgroundMusic").Stop();
                FindObjectOfType<SoundManager>().play("DesertBattleMusic");
            }
            if (Camera.main.GetComponent<AudioListener>() != null && manager.SoundMuted)
                Camera.main.GetComponent<AudioListener>().enabled = false;
            //Forest
            if (SceneManager.GetActiveScene().name == "Forest_FS")
            {
                FindSource("BackgroundMusic").Stop();
                FindObjectOfType<SoundManager>().play("ForestBattleMusic");
            }
            if (Camera.main.GetComponent<AudioListener>() != null && manager.SoundMuted)
                Camera.main.GetComponent<AudioListener>().enabled = false;
            //Savanna
            if (SceneManager.GetActiveScene().name == "Savannah_FS")
            {
                FindSource("BackgroundMusic").Stop();
                FindObjectOfType<SoundManager>().play("SavannaBattleMusic");
            }
            if (Camera.main.GetComponent<AudioListener>() != null && manager.SoundMuted)
                Camera.main.GetComponent<AudioListener>().enabled = false;
        }
    }

    public void play(string name)
    {
        Sounds s = Array.Find(Sounds, sound => sound.name == name);
        s.source.Play();
    }

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
