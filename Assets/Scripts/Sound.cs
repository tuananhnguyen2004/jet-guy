using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool isLoop;
    [Range(0, 1)]
    public float volume;
    [Range(0, 1)]
    public float pitch;
    [Range(0, 1)]
    [HideInInspector] public AudioSource source;
}
