using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env : MonoBehaviour
{
    public static Env Instance;

    private AudioSource EnvSrc;
    public AudioClip cityNoise;

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

        EnvSrc = GetComponent<AudioSource>();

    }

    public void cityNoiseAudio()
    {
        EnvSrc.clip = cityNoise;
        EnvSrc.volume = 0.05f;
        EnvSrc.pitch = 1f;
        EnvSrc.loop = true;
        EnvSrc.spatialBlend = 0.8f;

        EnvSrc.Play();
    }


    public void StopAudio()
    {
        EnvSrc.Stop();

        //Debug.Log("No Audio");
    }

}
