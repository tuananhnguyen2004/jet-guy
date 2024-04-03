using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    
    [SerializeField] List<Sound> sounds;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;
            s.source.clip = s.clip;
        }
        PlaySound("Theme");
    }

    public void PlaySound(string soundName)
    {
        Sound sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null)
            sound.source.Play();
    }

    public void EnableSound(string soundName)
    {
        Sound sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null)
            sound.source.enabled = true;
    }

    public void DisableSound(string soundName)
    {
        Sound sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null)
            sound.source.enabled = false;
    }

    public void StopSound(string soundName)
    {
        Sound sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null)
            sound.source.Stop();
    }

    public void SetVolume(string soundName, float volumeValue)
    {
        Sound sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null)
            sound.source.volume = volumeValue;
    }
}
