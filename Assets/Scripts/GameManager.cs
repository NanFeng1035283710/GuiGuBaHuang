using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ！
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public GameObject player_Prefab;
    public Vector2[] pos;
    public GameObject[] players;
    private void Start()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Instantiate(player_Prefab, pos[i],Quaternion.identity);
        }
    }
}
