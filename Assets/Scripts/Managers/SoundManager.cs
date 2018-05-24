using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    private AudioSource musicSource, playerSource, coinSource, powerupSource;
    [SerializeField] AudioClip soundtrack;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip endTrack, coinTrack, powerupTrack;

    void Awake() {
        instance = this;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = SaveLoad.LoadSoundVolume();
        musicSource.clip = soundtrack;
        musicSource.playOnAwake = true;
        musicSource.Play();

        playerSource = gameObject.AddComponent<AudioSource>();
        playerSource.volume = SaveLoad.LoadEffectsVolume();


        coinSource = gameObject.AddComponent<AudioSource>();
        coinSource.volume = SaveLoad.LoadEffectsVolume();

        powerupSource = gameObject.AddComponent<AudioSource>();
        powerupSource.volume = SaveLoad.LoadEffectsVolume();
    }

    public void PlayHit()
    {
        playerSource.clip = hitSound;
        playerSource.Play();
    }

    public void PlaySoundtrack()
    {
        musicSource.Play();
    }

    public void PauseSoundtrack() {
        musicSource.Pause();
    }

    public void PlayCoin() {
        coinSource.clip = coinTrack;
        coinSource.Play();
    }

    public void PlayPowerup() {
        powerupSource.clip = powerupTrack;
        powerupSource.Play();
    }
}
