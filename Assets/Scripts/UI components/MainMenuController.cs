using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField manualSeedInput;
    public Toggle seedOfTheDayToggle;
    public Toggle randomSeedToggle;

    void Start()
    {
        // Initialize input field with current manual seed value
        seedOfTheDayToggle.isOn = GameSettings.dateSeed;
        manualSeedInput.text = GameSettings.manualSeed.ToString();
    }

    public void OnManualSeedInput()
    {
        string inputText = manualSeedInput.text;

        try
        {
            int seed = int.Parse(inputText);
            GameSettings.UpdateManualSeed(seed);
            Debug.Log("Manual Seed Updated: " + GameSettings.manualSeed);
        }
        catch (FormatException)
        {
            Debug.LogWarning("Invalid seed input! Please enter a valid integer.");
        }
    }

        public void OnSeedOfTheDayToggle()
    {
        // Update dateSeed based on toggle state
        GameSettings.dateSeed = seedOfTheDayToggle.isOn;
        Debug.Log("Date Seed Toggled: " + GameSettings.dateSeed);
    }
    public void OnRandomSeedToggle()
    {
        // Update dateSeed based on toggle state
        GameSettings.randomSeed = randomSeedToggle.isOn;
        Debug.Log("Random Seed Toggled: " + GameSettings.randomSeed);
    }
}