using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerLocation;
    public PlayerController player;
    public PlayerData data;
    Scene scene;
    public int level = 1;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        level = scene.buildIndex;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player, this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        player.health = data.health;
        player.ammo = data.ammo;

        playerLocation.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
