using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] BGM, AnimEffect;
    public List<string> BGMName, AnimEffectName;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        LoadList(BGM, BGMName);
        LoadList(AnimEffect, AnimEffectName);



    }

    private void Start()
    {
        BGMPlay("cityNoise");
    }


    private void Update()
    {
        
    }


    //Common
    private void LoadList(Sound[] soundList, List<string> nameList)
    {
        foreach (Sound s in soundList)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.spatialBlend = s.SpatialBlend;

            nameList.Add(s.Name);
        }

    }


    


    //Anim
    public void AnimEffectPlay(string name)
    {
        Sound sound = Array.Find(AnimEffect, i => i.Name == name);

        if (sound == null)
        { Debug.LogFormat("Clip {0} not found", name); return; }

        sound.Source.Play();
        Debug.Log(sound.Name + " started");
    }

    public void AnimEffectPlayPart(string name, float startTime, float endTime)
    {
        Sound sound = Array.Find(AnimEffect, i => i.Name == name);

        if (sound == null)
        { Debug.LogFormat("Clip {0} not found", name); return; }

        sound.Source.Play();
        Debug.Log(sound.Name + " started");

        sound.Source.PlayScheduled(AudioSettings.dspTime + startTime);

        sound.Source.SetScheduledEndTime(AudioSettings.dspTime + endTime);
    }

    public void AnimEffectStop(string name)
    {
        Sound sound = Array.Find(AnimEffect, i => i.Name == name);

        if (sound == null)
        { Debug.LogFormat("Clip {0} not found", name); return; }

        sound.Source.Stop();
        Debug.Log(sound.Name + " stoped");
    }

    public List<string> AnimEffectGetName() {return AnimEffectName;}





    //BGM
    public void BGMPlay(string name)
    {
        Sound sound = Array.Find(BGM, i => i.Name == name);

        if (sound == null)
        { Debug.LogFormat("Clip {0} not found", name); return; }

        sound.Source.Play();
    }

    public void BGMStop(string name)
    {
        Sound sound = Array.Find(BGM, i => i.Name == name);

        if (sound == null)
        { Debug.LogFormat("Clip {0} not found", name); return; }

        sound.Source.Stop();
    }

    public List<string> BGMGetName() { return BGMName; }

}
