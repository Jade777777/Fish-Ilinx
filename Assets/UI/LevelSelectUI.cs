using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Transform levelRoot;
    [SerializeField] GameObject unlockedPrefab;
    [SerializeField] GameObject lockedPrefab;

    void Awake()
    {
        // Keep across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Find game manager and setup event listener
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.onStateUpdated += UpdateUI;
    }

    public void UpdateUI()
    {
        // Empty the level root
        foreach (Transform child in levelRoot)
        {
            Destroy(child.gameObject);
        }

        // Add the levels
        for (int i = 0; i < gameManager.GetLevelCount(); i++)
        {
            // Create the level and fill out text
            GameObject levelInstance = Instantiate(gameManager.GetLevelUnlocked(i)? unlockedPrefab : lockedPrefab, levelRoot);
            levelInstance.GetComponent<TextMeshPro>().text = "Level " + (i + 1);
        }
    }
}
