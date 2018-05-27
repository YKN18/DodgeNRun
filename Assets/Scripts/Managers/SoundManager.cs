using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    private AudioSource musicSource, playerSource, coinSource, powerupSource;
    [SerializeField] AudioClip soundtrack;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip endTrack, coinTrack, powerupTrack;

    void Awake() {
        instance = this;
        //Initializes the source for soundtrack and effets and plays the soundtrack
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
        //Plays the hit sound
        playerSource.clip = hitSound;
        playerSource.Play();
    }

    public void PlaySoundtrack()
    {
        //Plays the soundtrack (called by resume)
        musicSource.Play();
    }

    public void PauseSoundtrack() {
        //Pauses the soundtrack (called by pause)
        musicSource.Pause();
    }

    public void PlayCoin() {
        //Plays the coins sound effect
        coinSource.clip = coinTrack;
        coinSource.Play();
    }

    public void PlayPowerup() {
        //Plays the powerup sound effect
        powerupSource.clip = powerupTrack;
        powerupSource.Play();
    }
}
