using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public static UISound Instance;

    private AudioSource UISrc;
    public AudioClip ButtonClip;
    public AudioClip DropClip;
    public AudioClip ErrorClip;
    public AudioClip SuccessClip;

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

        UISrc = GetComponent<AudioSource>();

    }


    public void ButtonSound()
    {
        UISrc.clip = ButtonClip;
        UISrc.volume = 1f;
        UISrc.pitch = 1f;
        UISrc.spatialBlend = 0f;
        UISrc.loop = false;
        UISrc.Play();

    }

    public void DropSound()
    {
        UISrc.clip = DropClip;
        UISrc.volume = 1f;
        UISrc.pitch = 1f;
        UISrc.loop = false;
        UISrc.spatialBlend = 0f;
        UISrc.Play();

    }

    public void ErrorSound()
    {
        UISrc.clip = ErrorClip;
        UISrc.volume = 1f;
        UISrc.pitch = 1f;
        UISrc.loop = false;
        UISrc.spatialBlend = 0f;
        UISrc.Play();

    }

    public void SuccessSound()
    {
        UISrc.clip = SuccessClip;
        UISrc.volume = 1f;
        UISrc.pitch = 1f;
        UISrc.loop = false;
        UISrc.spatialBlend = 0f;
        UISrc.Play();
    }

    public void StopAudio()
    {
        UISrc.Stop();

    }
}
