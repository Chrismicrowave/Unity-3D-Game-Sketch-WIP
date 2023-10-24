using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM Instance;

    private AudioSource BGMSrc;
    public AudioClip bgm01;

    void Awake()
    {
        if (Instance == null)
        { Instance = this; }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);

        BGMSrc = GetComponent<AudioSource>();

    }

    
    public void BGM01Audio()
    {
        BGMSrc.clip = bgm01;
        BGMSrc.volume = 0.1f;
        BGMSrc.pitch = 1f;
        BGMSrc.loop = true;
        BGMSrc.spatialBlend = 0f;

        BGMSrc.Play();
    }


    public void StopAudio()
    {
        BGMSrc.Stop();

        //Debug.Log("No Audio");
    }

}
