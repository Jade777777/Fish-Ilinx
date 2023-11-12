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
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.onStateUpdated += UpdateUI;
        UpdateUI();
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
            // Create the level instance
            GameObject levelInstance = Instantiate(gameManager.GetLevelUnlocked(i)? unlockedPrefab : lockedPrefab, levelRoot);

            // Set text for level & fish count
            levelInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + i;
            levelInstance.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameManager.GetCollectedFish(i) + "/" + gameManager.GetTotalFish(i);
        }
    }
}
