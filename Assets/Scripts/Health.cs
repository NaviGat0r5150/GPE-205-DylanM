using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public delegate void EnemyKilledEventHandler();
    public static event EnemyKilledEventHandler OnEnemyKilled;

    public float currentHealth;
    public float maxHealth = 100;
    public float maxLives = 3;
    public float currentLives;

    public float currentScore;

    public Image healthAmountImage;
    private float currentHealthPercentage;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;

    private Health playerHealth;

    public AudioSource dieSound;
    public AudioSource hurtSound;
    public AudioSource healSound;

    public bool hasShield = false;
    private bool isDead = false;

    public string sceneName; // Name of the scene to load
    public KeyCode switchKey = KeyCode.F; // Key to trigger scene switch

    public void Start()
    {
        playerHealth = GetComponent<Health > ();
        if (playerHealth != null)
        {
            Health.OnEnemyKilled += IncrementScore;
        }
    }
    void Awake()
    {
        currentHealth = maxHealth;
        currentLives = maxLives;
        currentScore = 0;


        if (LivesText == null || ScoreText == null)
        {
            Debug.LogError("One or both of the Text components is not assigned!");
        }

    }

    public void Update()
    {
        currentHealthPercentage = currentHealth / maxHealth;
        healthAmountImage.fillAmount = currentHealthPercentage;


        LivesText.text = "Lives: " + currentLives;

        ScoreText.text = "Score: " + currentScore;

        checkHealth();

        if (Input.GetKeyDown(KeyCode.F))
        {
            // Load the specified scene
            SceneManager.LoadScene("WIN");

        }
    }




    public void TakeDamage(float amount, Pawn source)
    {
        if (!hasShield) // Check if the shield is not active
        {
            currentHealth -= amount;
            Debug.Log(source.name + "did " + amount + " damage to " + gameObject.name);

            hurtSound.PlayOneShot(hurtSound.clip);
        }
        else
        {
            Debug.Log("Shield is active. No damage taken."); //shioeld active
        }



        if (currentHealth <= 0)
        {
            currentLives -= 1;
            dieSound.PlayOneShot(dieSound.clip);

            gameObject.transform.position = new Vector3(0f, 20f, 0f);

            if (currentLives <= 0)
            {
                Die(source);// Argument - the actual value passed into a function call


            }
            else
            {
                currentHealth = maxHealth;

            }
        }
    }

    public void RepenishHealth(float amount)
    {
        healSound.PlayOneShot(healSound.clip);
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(gameObject.name + "healed " + amount + "health");


    }

    public void Die(Pawn source)
    {
        dieSound.PlayOneShot(dieSound.clip);

        if (gameObject.CompareTag("Player"))
        {
            // Store the final score in PlayerPrefs
            PlayerPrefs.SetFloat("FinalScore", currentScore);
            // Load the game over scene
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            // Notify listeners that an enemy was killed
            OnEnemyKilled?.Invoke();
            // Destroy this game object
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            Health.OnEnemyKilled -= IncrementScore;
        }
    }

    void IncrementScore()
    {
        playerHealth.currentScore += 1;
    }

    public void checkHealth()
    {
        if (currentHealth <= 0)
        {
            currentLives -= 1;
            dieSound.PlayOneShot(dieSound.clip);

            gameObject.transform.position = new Vector3(0f, 20f, 0f);

            if (currentLives <= 0)
            {

                //this BREAKS THE GAME


            }
            else
            {
                currentHealth = maxHealth;

            }
        }
    }
}

