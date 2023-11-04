using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] int scene;
    public void Transition()
    {
        SceneManager.LoadScene(scene);
    }

}
