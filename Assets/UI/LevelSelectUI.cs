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
    TransitionToLevel transitionToLevel;

    int nextLevel; 
    void Start()
    {
        // Find game manager and setup event listener
        gameManager = GameObject.FindObjectOfType<GameManager>();
        transitionToLevel = FindObjectOfType<TransitionToLevel>();
        //gameManager.onStateUpdated += UpdateUI;
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
        for (int i = 2; i < gameManager.GetLevelCount()+2; i++)
        {
            // Create the level instance
            GameObject levelInstance = Instantiate(gameManager.IsLevelUnlocked(i) ? unlockedPrefab : lockedPrefab, levelRoot);

            // Set text for level
            levelInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + (i-1);

            if (gameManager.IsLevelUnlocked(i))
            {
                // Set text for fish count
                levelInstance.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + gameManager.GetCollectedFish(i) + "/" + gameManager.GetTotalFish(i);

                // Add button
                int thisLevel =  i;
                Button button = levelInstance.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    nextLevel = thisLevel;
                    transitionToLevel.SetUpMenuTransition();
                    Invoke( "SetCurrentLevel", 0.8f);
                    gameObject.SetActive(false);
                });
            }
        }
    }

    public void SetCurrentLevel()
    {
        gameManager.SetCurrentLevel(nextLevel);
    }
}
