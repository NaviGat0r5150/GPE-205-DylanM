using UnityEngine;

public class TitleScreenMusic : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component 
        audioSource = GetComponent<AudioSource>();

        // Play the audio
        audioSource.Play();
    }
}