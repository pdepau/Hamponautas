using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    static public AudioHolder Instance { get; private set; }

    public AudioSource Source;
    public AudioClip StartingSong; // Agrega esta variable para la canci�n de inicio.

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

    private void Start()
    {
        // Reproduce la canci�n de inicio autom�ticamente al inicio de la escena.
        if (StartingSong != null)
        {
            Play(StartingSong);
        }
    }
}