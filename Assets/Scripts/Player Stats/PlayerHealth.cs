using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PlayerHealth")]

public class PlayerHealth : ScriptableObject
{
    public int defaultHealth;

    public int playerHealth;

    public void OnEnable()
    {
        playerHealth = defaultHealth;
    }

    public void OnDisable()
    {
        playerHealth = 0;
    }

    public void OnDestroy()
    {
        playerHealth = 0;
    }

    // Returns the current health of the player
    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    // Resets the player's health back to the default max
    public void ResetPlayerHealth()
    {
       playerHealth = defaultHealth;
    }

    // Apply modification to the player's health (use negative values to reduce health)
    public void ModifyPlayerHealth(int value)
    {
        playerHealth = playerHealth > 0 ? playerHealth + value : 0;
    }
}
