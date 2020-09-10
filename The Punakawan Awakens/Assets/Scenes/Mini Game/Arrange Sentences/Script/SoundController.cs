using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public string audioClipsPath;
    [SerializeField] private AudioClip[] audioClips;

    private void Awake()
    {
        audioClips = Resources.LoadAll<AudioClip>(audioClipsPath);
    }
}
