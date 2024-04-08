using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    static public AudioHolder Instance { get; private set; }


    public AudioSource Source;

    public void Play(AudioClip clip)
    {
        Source.clip = clip;
        Source.Play();
    }
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void OnApplicationQuit()
    {
        OnDestroy();
    }

}
