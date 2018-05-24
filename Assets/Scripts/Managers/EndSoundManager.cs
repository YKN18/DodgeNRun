using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSoundManager : MonoBehaviour {

    public static EndSoundManager instance;
    private AudioSource musicSource;
    [SerializeField] AudioClip soundtrack;

    void Awake()
    {
        instance = this;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = SaveLoad.LoadSoundVolume();
        musicSource.clip = soundtrack;
        musicSource.playOnAwake = true;
        musicSource.Play();
    }
}
