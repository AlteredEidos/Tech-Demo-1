using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Scene scene;
    public int level;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        level = scene.buildIndex;
    }


}
