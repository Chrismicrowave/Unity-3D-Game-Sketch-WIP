using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    //writing to Source parameter

    public string Name;

    public AudioClip Clip;

    [Range(0f,1f)]
    public float Volume;

    [Range(.1f,3f)]
    public float Pitch;

    public bool Loop;

    [Range(0f, 1f)]
    public float SpatialBlend;

    public AudioSource Source;
}
