using UnityEngine;
using UnityEngine.UI;

public class SoundVolume: MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        float savedVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        slider.value = savedVolume;
        // Subscribe to the OnValueChanged event of the slider
        slider.onValueChanged.AddListener(OnSoundVolumeChanged);
    }

    private void OnSoundVolumeChanged(float volume)
    {
        // Set sound volume
        AudioListener.volume = volume;
        // Save sound volume to PlayerPrefs
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
}
