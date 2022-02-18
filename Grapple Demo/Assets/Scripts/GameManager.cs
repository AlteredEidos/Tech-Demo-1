using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerLocation;
    public PlayerController player;
    public PlayerData data;
    public GameObject menueText;
    public GameObject resumeButton;
    public GameObject loadButton;
    public GameObject quitButton;
    private bool paused;

    Scene scene;
    public int level;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        level = scene.buildIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false && level != 0)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            Resume();
        }
    }

    public void Pause()
    {
        menueText.SetActive(true);
        resumeButton.SetActive(true);
        loadButton.SetActive(true);
        quitButton.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void Resume()
    {
        menueText.SetActive(false);
        resumeButton.SetActive(false);
        loadButton.SetActive(false);
        quitButton.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    //temp load
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player, this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;

        SceneManager.LoadScene(level);

        playerLocation.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        player.spawnPos = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
