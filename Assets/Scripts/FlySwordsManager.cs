using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞剑管理
/// </summary>
public class FlySwordsManager : Singleton<FlySwordsManager>
{
    public GameObject[] flySwords;
    public Dictionary<string,GameObject> flySwordsDic =new Dictionary<string, GameObject>();
    private void Start()
    {
        for (int i = 0; i < flySwords.Length; i++)
        {
            flySwordsDic.Add(flySwords[i].name, flySwords[i]);
        }
    }
    /// <summary>
    /// 创建飞剑
    /// </summary>
    /// <param name="name"></param>
    public GameObject CreateFlySword(string name)
    {
        flySwordsDic.TryGetValue(name, out GameObject flySword);
        return Instantiate(flySword,transform);
    }
}
