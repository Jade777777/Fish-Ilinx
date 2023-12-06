using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    int onboardingIndex =0;
    int testLevelIndex = 8;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if(gameManager != null) Destroy(gameManager.gameObject);
            SceneManager.LoadScene(onboardingIndex);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            Destroy(gameManager.gameObject);
            SceneManager.LoadScene(testLevelIndex);
        }
    }
}
