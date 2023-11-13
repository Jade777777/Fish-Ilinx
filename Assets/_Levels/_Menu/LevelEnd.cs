using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    OrbitPath orbitPath;
    public BoidProcess process;
    public TMP_Text score;
    GameManager gameManager;
    int savedFish;
    // Start is called before the first frame update
    int totalFish;
    void Start()
    {
        orbitPath = GetComponent<OrbitPath>();
        gameManager = FindObjectOfType<GameManager>();
        int level = gameManager.GetCurrentLevel();
        totalFish = gameManager.GetTotalFish(level);
    }

    // Update is called once per frame
    void Update()
    {
        int fish = process.boidGridPartition.GetBoidsInRange(transform.position, orbitPath.MaxDistance).Count - 1;
        if (savedFish < fish)
        {
            savedFish = fish;
        }
        

        score.text= "Fish Found: " + savedFish.ToString() + "/" + totalFish ;
    }

   private void UnlockAndSaveScore()
    {
        int level = gameManager.GetCurrentLevel();
        gameManager.SetLevelUnlocked(level + 1, true);
        gameManager.SetHighScore(level, savedFish);
    }
    public void RestartLevel()
    {
        UnlockAndSaveScore();
        int level = gameManager.GetCurrentLevel();
        gameManager.SetCurrentLevel(level);
    }
    public void ReturnToMenu()
    {
        UnlockAndSaveScore();
        gameManager.ReturnToMainMenu();
    }
}
