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
    List<int> nums = new List<int>();

    void Start()
    {
        // Find game manager and setup event listener
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.onStateUpdated += UpdateUI;

        // Fill out list
        for (int i = 0; i < gameManager.GetLevelCount(); i++)
        {
            nums.Add(i);
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        // Empty the level root
        foreach (Transform child in levelRoot)
        {
            Destroy(child.gameObject);
        }

        // Add the levels -- must use the list compared to iterating
        foreach (int num in nums)
        {
            Debug.Log(num);
            // Create the level instance
            GameObject levelInstance = Instantiate(gameManager.IsLevelUnlocked(num) ? unlockedPrefab : lockedPrefab, levelRoot);

            // Set text for level & fish count
            levelInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + num;
            levelInstance.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = gameManager.GetCollectedFish(num) + "/" + gameManager.GetTotalFish(num);

            // Add button
            if (gameManager.IsLevelUnlocked(num))
            {
                Debug.Log("Set button to " + num);
                Button button = levelInstance.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    gameManager.SetCurrentLevel(num, 10); 
                    gameObject.SetActive(false);
                });
            }
        }
    }
}
