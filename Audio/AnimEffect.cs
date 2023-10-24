using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEffect : MonoBehaviour
{
    public static AnimEffect Instance;

    private AudioSource AnimSrc;
    public AudioClip isRun;
    public AudioClip isWalk;
    public AudioClip isLanded;

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

        AnimSrc = GetComponent<AudioSource>();

    }


    public void IsLandedAudio()
    {
        AnimSrc.clip = isLanded;
        AnimSrc.volume = 3f;
        AnimSrc.pitch = 1f;
        AnimSrc.spatialBlend = 0.5f;
        AnimSrc.loop = false;
        AnimSrc.Play();

        //Debug.Log("Landed Audio");
    }

    public void IsRunAudio()
    {
        AnimSrc.clip = isRun;
        AnimSrc.volume = 3f;
        AnimSrc.pitch = 1f;
        AnimSrc.loop = true;
        AnimSrc.spatialBlend = 0.5f;
        AnimSrc.Play();

        //Debug.Log("Run Audio");
    }

    public void IsWalkAudio()
    {
        AnimSrc.clip = isRun;
        AnimSrc.volume = 3f;
        AnimSrc.pitch = 0.5f;
        AnimSrc.loop = true;
        AnimSrc.spatialBlend = 0.5f;
        AnimSrc.Play();

        //Debug.Log("Walk Audio");
    }

    public void StopAudio()
    {
        AnimSrc.Stop();

        //Debug.Log("No Audio");
    }
}
