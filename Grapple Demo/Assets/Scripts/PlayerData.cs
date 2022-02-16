using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int ammo;
    public float[] position;

    public PlayerData(PlayerController player, GameManager game)
    {
        health = player.health;
        ammo = player.ammo;
        level = game.level;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
