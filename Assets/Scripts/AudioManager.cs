using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            // set the volume & pitch
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        // find the sound that needs to be played
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // choose a random clip from the array
        int index = UnityEngine.Random.Range(0, s.clips.Length);
        s.source.clip = s.clips[index];

        // play it
        s.source.Play();
    }
}