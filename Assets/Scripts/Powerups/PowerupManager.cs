using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{

    #region Variables
    public List<Powerup> powerups;
    public List<Powerup> removedPowereupQueue;

    public AudioSource powerUpSound;
    public AudioSource powerDownSound;

    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {

        //be sure to initialize the list
        powerups = new List<Powerup>();
        removedPowereupQueue = new List<Powerup>();
    }

    // Update is called once per frame

    private void Update()
    {
        DecrementPowerupTimers();
    }
    void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }

    public void Add (Powerup powerupToAdd)
    {
        //Create add method
        powerupToAdd.Apply(this);

        powerUpSound.PlayOneShot(powerUpSound.clip);

        powerups.Add(powerupToAdd);
    }

    public void Remove(Powerup powerupToRemove)
    {
        // TO DO Create removal method
        powerupToRemove.Remove(this);

        powerDownSound.PlayOneShot(powerDownSound.clip);

        //get ready to remove it from the list
        removedPowereupQueue.Add(powerupToRemove);
    }

    public void DecrementPowerupTimers()
    {
        foreach (Powerup powerup in powerups)
        {
            powerup.duration -= Time.deltaTime;

            if (powerup.duration < 0)
                Remove(powerup);
        }
    }

    private void ApplyRemovePowerupsQueue()
    {
        foreach(Powerup powerup in removedPowereupQueue) 
        { 
            powerups.Remove(powerup);
        }

        removedPowereupQueue.Clear();
    }
}
