using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视口
/// </summary>
public class Viewport : Singleton<Viewport>
{
    float minX;
    float maxX;
    float minY;
    float maxY;
    float middleX;
    public float MaxX => maxX;
    private void Start()
    {
        ///将当前场景主摄像机赋值 获取摄像机对象
        Camera mainCamera = Camera.main;
        //把左下角视口坐标转换成世界坐标
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0,0));
        //把右上角视口坐标转换成世界坐标  通过视口坐标得到世界位置
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));
        //左下角视口坐标就是世界位置的最小值
        minX = bottomLeft.x;
        minY = bottomLeft.y;
        //右上角视口坐标就是世界位置的最大值
        maxX = topRight.x;
        maxY = topRight.y;
        middleX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).x;
        Debug.Log(bottomLeft);
        Debug.Log(topRight);
    }

    /// <summary>
    /// 限定玩家可移动的范围
    /// </summary>
    /// <param name="PlayerPosition">玩家位置</param>
    /// <returns></returns>
    public Vector3 PlayerMoveablePosition(Vector3 PlayerPosition, float paddingx, float paddingy)
    {
        Vector3 position = Vector3.zero;
        // Mathf.Clamp(给的值,最小值,最大值)  如果给的值超过了最小值 则返回最小值
        position.x = Mathf.Clamp(PlayerPosition.x, minX + paddingx, maxX - paddingx);
        position.y = Mathf.Clamp(PlayerPosition.y, minY + paddingy, maxY - paddingy);
        return position;
    }
    /// <summary>
    /// 返回一个敌人随机生成的位置
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomEnemySpawnPosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.one;
        position.x = Random.Range(minX + paddingX, MaxX - paddingX);;
        position.y = 1.7f;
        position.z = Random.Range(-minY + paddingY, maxY - paddingY);
        return position;
    }
    /// <summary>
    /// 随机右半部分的位置
    /// </summary>
    /// <returns></returns>
    public Vector3 RandomRightHalfPosition(float paddingX, float paddingY)
    {
        Vector3 position = Vector3.zero;
        position.x = Random.Range(middleX, maxX - paddingX);
        position.y = Random.Range(minY + paddingY, maxY - paddingY);
        return position;
    }
    public Vector3 RandomPostion()
    {
        Vector3 position = Vector3.zero;
        position.x = Random.Range(minX + 3f, maxX - 3f);
        position.y = Random.Range(minY + 3f, maxY - 3f);
        return position;
    }
}
