using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ！
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Transform target;
   [Tooltip("攻击模式")] public int patternIndex = 0;
    private ProjectilesWeapon projectilesWeapon;

    private void Start()
    {
        projectilesWeapon = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<ProjectilesWeapon>();
        projectilesWeapon.target= target;
        projectilesWeapon.patternIndex = patternIndex;
        projectilesWeapon.gameObject.SetActive(true);
    }
}
