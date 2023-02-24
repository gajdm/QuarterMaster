using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<string> audioStrings = new List<string>();
    public void Start()
    {
        for(int i = 0;i<audioStrings.Count;i++)
        {
            audioDictionary.Add(audioStrings[i],audioClips[i]);
        }
    }
    public void PlaySound(string sound)
    {
        audioSource.PlayOneShot(audioDictionary[sound]);
    }
}
