using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    Object startScene;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SavedFishSpawn.savedFish = 0;
            SceneManager.LoadScene(0);
        }
    }
}
