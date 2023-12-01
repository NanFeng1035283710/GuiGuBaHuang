using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制
/// </summary>
public class PlayerController : MonoBehaviour
{

    [Tooltip("攻击模式")]
    public int patternIndex = 0;
    [Tooltip("id")]
    public int id = 0;
    private int layer;

    /// <summary>
    /// 轨道点
    /// </summary>
    public Transform[] trackPoints;
    /// <summary>
    /// 是否是左边轨道
    /// </summary>
    private bool IsLeft;
    /// <summary>
    /// 飞剑
    /// </summary>
    public List<FlySwordController> flySwords;
    private ProjectilesWeapon projectilesWeapon;
    public Transform enemy;

    private void Start()
    {
        layer = id + 10;
        transform.gameObject.layer = layer;
        RandomProject();
    }
    /// <summary>
    /// 随机子弹
    /// </summary>
    private void RandomProject()
    {
        //随机子弹
        projectilesWeapon = transform.GetChild(Random.Range(0, transform.childCount - 1)).GetComponent<ProjectilesWeapon>();
        //随机攻击模式
        projectilesWeapon.patternIndex = patternIndex;
        projectilesWeapon.gameObject.SetActive(true);
        projectilesWeapon.SetParticleCollisionLayer(layer);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //如果碰到飞剑

        if (collision.gameObject.layer == 3)
        {
            GetFlySword(collision.gameObject.tag);
            FlySwordsManager.Instance.flySwordsTrophies.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// 获得飞剑
    /// </summary>
    private void GetFlySword(string name)
    {
        GameObject flySword = FlySwordsManager.Instance.CreateFlySword(name);
        //设置 层级
        flySword.layer = layer;
        FlySwordController flySwordController = flySword.GetComponent<FlySwordController>();
        if (flySwords.Count == 2)
        {
            //销毁第一个
            Destroy(flySwords[0].gameObject);
            //移除第一个
            flySwords.RemoveAt(0);
        }
        //加入列表
        flySwords.Add(flySwordController);
        //设置轨道
        flySwordController.trackPoint = trackPoints[IsLeft ? 0 : 1];
        IsLeft = !IsLeft;
        //设置主玩家
        flySwordController.playerPoint = transform;
    }
}
