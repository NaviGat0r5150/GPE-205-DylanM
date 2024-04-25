using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    // Seed for random number generation
    public static int manualSeed;
    public static bool randomSeed = false;
    public static bool dateSeed = false;

    //Method to update the manual seed
    public static void UpdateManualSeed(int newSeed)
    {
        manualSeed = newSeed;
    }
}
