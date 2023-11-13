using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    int onboardingIndex =0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            Destroy(gameManager.gameObject);
            SceneManager.LoadScene(onboardingIndex);
        }
    }
}
