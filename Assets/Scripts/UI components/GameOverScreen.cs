using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        // Retrieve the final score from PlayerPrefs
        float finalScore = PlayerPrefs.GetFloat("FinalScore");
        // Display the final score
        scoreText.text = "Final Score: " + finalScore.ToString();
    }
}