using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    private Slider slider;
    public AudioSource musicSource;

    private void Start()
    {
        slider = GetComponent<Slider>();

        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        slider.value = savedVolume;
        // Subscribe to the OnValueChanged event of the slider
        slider.onValueChanged.AddListener(OnMusicVolumeChanged);

        // Set the initial music volume using AudioManager
        AudioManager.instance.SetMusicVolume(savedVolume);
    }

    private void OnMusicVolumeChanged(float volume)
    {
        // Set music volume using AudioManager
        AudioManager.instance.SetMusicVolume(volume);
        // Save music volume to PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}