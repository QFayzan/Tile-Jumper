using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public AudioClip clip;

    [Range(0,1)] public float volume;
    [Range(0.1f,3f)]public float pitch;
    public string name;
    [HideInInspector] public AudioSource source;

}
