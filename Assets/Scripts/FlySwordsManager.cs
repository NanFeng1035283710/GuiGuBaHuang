using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞剑管理
/// </summary>
public class FlySwordsManager : Singleton<FlySwordsManager>
{
    /// <summary>
    /// 飞剑预制件
    /// </summary>
    public GameObject[] flySword_Prefab;
    /// <summary>
    /// 飞剑战利品预制件
    /// </summary>
    public GameObject[] flySwordsTrophie_Prefabs;
    public Dictionary<string, GameObject> flySwordsDic = new Dictionary<string, GameObject>();
    public int maximum;


    public List<GameObject> flySwordsTrophies = new List<GameObject>();
    /// <summary>
    /// 掉落率
    /// </summary>
    [Range(0, 100)] public float rewardProbability;
    /// <summary>
    /// 最大数量
    /// </summary>
    private WaitForSeconds waitFor;
    private void Start()
    {

        waitFor = new WaitForSeconds(1);
        for (int i = 0; i < flySword_Prefab.Length; i++)
        {
            flySwordsDic.Add(flySword_Prefab[i].name, flySword_Prefab[i]);
        }
        StartCoroutine(nameof(FlySwordsTrophieCorouinte));
    }

    /// <summary>
    /// 飞剑战利品掉落几率
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlySwordsTrophieCorouinte()
    {
        while (true)
        {
            if (flySwordsTrophies.Count < maximum)
            {
                if (Random.Range(0, 100) < rewardProbability)
                {
                    //在屏幕中随机地点生成
                    GameObject go = Instantiate(flySwordsTrophie_Prefabs[Random.Range(0, flySwordsTrophie_Prefabs.Length)], transform);
                    Vector3 vector3= Viewport.Instance.RandomEnemySpawnPosition(5, 5);
                    go.transform.position = vector3;
                    Debug.Log(vector3);
                    Debug.Log(go.transform.position);
                    flySwordsTrophies.Add(go);
                }
            }
            yield return waitFor;
        }
    }
    /// <summary>
    /// 创建飞剑
    /// </summary>
    /// <param name="name"></param>
    public GameObject CreateFlySword(string name)
    {
        flySwordsDic.TryGetValue(name, out GameObject flySword);
        return Instantiate(flySword, transform);
    }
}
