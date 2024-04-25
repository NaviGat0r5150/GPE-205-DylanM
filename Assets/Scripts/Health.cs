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

    public float currentScore = 0;

    public Image healthAmountImage;
    private float currentHealthPercentage;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;

    private Health playerHealth;

    // Start is called before the first frame update

    public void Start()
    {
        playerHealth = GetComponent<Health>();
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
    }




    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth -= amount;
        Debug.Log(source.name + "did " + amount + " damage to " + gameObject.name);



        if (currentHealth <= 0)
        {
            currentLives -= 1;


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
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(gameObject.name + "healed " + amount + "health");


    }

    public void Die(Pawn source)
    {
        // Check if the GameObject has a "Player" tag before loading the scene
        if (gameObject.CompareTag("Player"))
        {
            // Load the "GameOver" scene
            SceneManager.LoadScene("GameOver");
            Destroy(gameObject);
        }
        else
        {
            // If the GameObject does not have a "Player" tag, destroy it
            Destroy(gameObject);

            if (OnEnemyKilled != null)
            {
                OnEnemyKilled(); // Trigger the event
            }

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
    }

  

