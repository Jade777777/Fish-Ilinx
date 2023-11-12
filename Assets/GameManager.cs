using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int levels; // Amount of levels
    private int currentLevel = 0;
    [SerializeField] private int[] totalFish; // Per level
    private int[] collectedFish; // Per level
    private bool[] isLevelUnlocked; // Per level
    public event Action onStateUpdated; // Indicator for UI to update

    void Awake()
    {
        // Keep across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Initialize arrays
        collectedFish = new int[levels];
        isLevelUnlocked = new bool[levels];

        // Fill out arrays -- load data later?
        Array.Fill(collectedFish, 0);
        Array.Fill(isLevelUnlocked, false);
        isLevelUnlocked[0] = true;

        // Set level and load
        currentLevel = 0;
        // SceneManager.LoadScene(currentLevel);
    }

    public int GetLevelCount()
    {
        return levels;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetCollectedFish(int level)
    {
        return collectedFish[level];
    }

    public int GetTotalFish(int level)
    {
        return totalFish[level];
    }

    public void SetCurrentLevel(int newLevel, int fishCollected)
    {
        // Update fish collected
        if (fishCollected != 0)
        {
            collectedFish[currentLevel] += fishCollected;
            onStateUpdated();
        }

        // Change the level
        currentLevel = newLevel;
        SceneManager.LoadScene(currentLevel);
    }

    public bool GetLevelUnlocked(int level)
    {
        return isLevelUnlocked[level];
    }

    public void SetLevelUnlocked(int level, bool isUnlocked)
    {
        // Update state if a level gets unlocked
        if (isLevelUnlocked[level] != isUnlocked) onStateUpdated();
        isLevelUnlocked[level] = isUnlocked;
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
