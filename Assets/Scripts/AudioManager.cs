using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource, sfxSource;
    public GameObject database;

    public void PlaySFX(int id)
    {
        sfxSource.clip = database.GetComponent<CardDatabase>().cardAudios[id];
        sfxSource.Play();
    }

    public void PlayMusic()
    {
        musicSource.clip = database.GetComponent<CardDatabase>().background;
        musicSource.Play();
    }
}
