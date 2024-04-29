using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance

    public AudioSource mainMenuMusic;
    public AudioSource inGameMusic;
    public AudioSource endGameMusic;

    private float musicVolume = 1.0f; // Default volume

    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        mainMenuMusic.volume = volume;
        // Adjust only in-game music volume

        inGameMusic.volume = volume;
        endGameMusic.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    // Method to adjust in-game music volume separately
    public void SetInGameMusicVolume(float volume)
    {
        inGameMusic.volume = volume;
    }
}