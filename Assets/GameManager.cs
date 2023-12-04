using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    
    //[SerializeField] private int levels; // Amount of levels
    private int currentLevel = 0;
    [SerializeField] private List<int> totalFish; // Per level
    private int[] collectedFish; // Per level
    private bool[] isLevelUnlocked; // Per level
    
    public event Action onStateUpdated; // Indicator for UI to update

    void Awake()
    {
        
        
        onStateUpdated = () => { };
        // Keep across scenes
        DontDestroyOnLoad(this.gameObject);

        // Initialize arrays
        collectedFish = new int[totalFish.Count];
        isLevelUnlocked = new bool[totalFish.Count];

        // Fill out arrays -- load data later?
        Array.Fill(collectedFish, 0);
        Array.Fill(isLevelUnlocked, false);
        isLevelUnlocked[0] = true;//The first level has a scene index of 2

        // Set level
        currentLevel = 0;
    }

    public int GetLevelCount()
    {
        return totalFish.Count;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetCollectedFish(int level)
    {
        return collectedFish[level-2];
    }
    public int GetCollectedFishFromAllLevels()
    {
        int total = 0;
        for (int i = 0; i < totalFish.Count; i++)
        {
            total += collectedFish[i];
        }
        return total;
    }

    public int GetTotalFish(int level)
    {
        return totalFish[level-2];
    }

    public void SetCurrentLevel(int newLevel)
    {
        // Update fish collected

        // Change the level
        currentLevel = newLevel;
        SceneManager.LoadScene(currentLevel);
    }
    public void ReturnToMainMenu()
    {
        //We go to a copy of the main menu so that the game manager doesn't get instantiated twice
        currentLevel = 1;
        SceneManager.LoadScene(1);
    }
    public void SetHighScore(int level, int score)
    {
        if (score > collectedFish[currentLevel - 2])
        {
            collectedFish[currentLevel-2] = score;
            onStateUpdated();
        }


        //if (score > PlayerPrefs.GetInt("HighScore" + level.ToString()))
        //{
        //    PlayerPrefs.SetInt("HighScore" + level.ToString(), score);
        //}
    }

    public bool IsLevelUnlocked(int level)
    {
        return isLevelUnlocked[level -2];
    }

    public void SetLevelUnlocked(int level, bool isUnlocked)
    {
        // Update state if a level gets unlocked
        if (isLevelUnlocked.Length > level-2)
        {
            if (isLevelUnlocked[level - 2] != isUnlocked) onStateUpdated();
            isLevelUnlocked[level - 2] = isUnlocked;
        }
    }

    public void ResetValues()
    {
        Array.Fill(collectedFish, 0);
        Array.Fill(isLevelUnlocked, false);
        isLevelUnlocked[0] = true;
        onStateUpdated();
    }

    public void ResetGame()
    {
        ResetValues();
        currentLevel = 0;
        SceneManager.LoadScene(currentLevel);
    }
}
