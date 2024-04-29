using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource buttonClickSound;

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.PlayOneShot(buttonClickSound.clip);
        }
    }
}
