using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Transform levelRoot;
    [SerializeField] GameObject unlockedPrefab;
    [SerializeField] GameObject lockedPrefab;

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
            GameObject levelInstance = Instantiate(gameManager.IsLevelUnlocked(i) ? unlockedPrefab : lockedPrefab, levelRoot);

            // Set text for level & fish count
            levelInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + i;
            levelInstance.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameManager.GetCollectedFish(i) + "/" + gameManager.GetTotalFish(i);

            // Add button
            if (gameManager.IsLevelUnlocked(i))
            {
                int thisLevel =  i;
                Button button = levelInstance.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    gameManager.SetCurrentLevel(thisLevel, 10);
                    gameObject.SetActive(false);
                });
            }
        }
    }
}
