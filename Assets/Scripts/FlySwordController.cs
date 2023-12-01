using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 飞剑控制器
/// </summary>
public class FlySwordController : MonoBehaviour
{
    [Header("======移动======")]
    /// <summary>
    /// 最小弹道角度
    /// </summary>
    [SerializeField] float minBallisticAngle = 50f;
    /// <summary>
    /// 最大弹道角度
    /// </summary>
    [SerializeField] float maxBallisticAngle = 75f;
    /// <summary>
    /// 随机弹道角度
    /// </summary>
    float ballisticAngle;
    /// <summary>
    /// 飞行速度
    /// </summary>
    [SerializeField] float moveSpeed;
    /// <summary>
    /// 环绕旋转速度
    /// </summary>
    [SerializeField] float rotateSpeed;

    [Header("======攻击======")]
    [SerializeField] float atkTimer;


    public Transform playerPoint;
    /// <summary>
    /// 轨道点
    /// </summary>
    public Transform trackPoint;
    public Transform enemyPoint;
    /// <summary>
    /// 目标点
    /// </summary>
    private Vector3 targetDirection;
    private float timer;
    private bool isAtk;
    /// <summary>
    /// 最短距离
    /// </summary>
    private float shortestDistance;
    //拾取.. 
    //飞到轨道点
    //到达目标点后围绕飞行
    //到达时间后  飞到目标点
    //飞到轨道点

    //从玩家列表寻找敌人
    //飞往敌人点

    private void Start()
    {
        StartCoroutine(FlyTargetPointCorouinte(trackPoint));
    }
    /// <summary>
    /// 飞到目标点
    /// </summary>
    /// <param name="target">目标点</param>
    /// <returns></returns>
    private IEnumerator FlyTargetPointCorouinte(Transform target)
    {
        while (true)
        {
            if (isAtk)
            {
                SetEnemy();
                while (enemyPoint != null && Vector3.Distance(transform.position, enemyPoint.position) > 0.5f && enemyPoint.gameObject.activeSelf)
                {
                    targetDirection = enemyPoint.position - transform.position;
                    //获取旋转角度  原点距离目标点的夹角 返回一个弧度   将弧度转换为角度
                    var angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
                    //设置子弹的旋转角度(角度,围绕旋转的轴)
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                    // 叠加旋转
                    transform.rotation *= Quaternion.Euler(0f, ballisticAngle, 0f);
                    //移动子弹
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    yield return null;
                }
                    isAtk = false;
                    timer = 0;
            }
            else
            {   //飞往轨道点
                if (Vector3.Distance(transform.position, target.position) > 0.1f)
                {  //目标方向等于目标位置减去自身位置
                    targetDirection = target.position - transform.position;
                    //获取旋转角度  原点距离目标点的夹角 返回一个弧度   将弧度转换为角度
                    var angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
                    //设置子弹的旋转角度(角度,围绕旋转的轴)
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                    // 叠加旋转
                    transform.rotation *= Quaternion.Euler(0f, 0, 0f);
                    //移动子弹
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    yield return null;
                }
                else  //环绕轨道飞行
                {
                    targetDirection = playerPoint.position - transform.position;
                    var angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.up);
                    transform.position = trackPoint.position;
                    // transform.RotateAround(playerPoint.position, Vector3.up, rotateSpeed * Time.deltaTime);
                    yield return new WaitForFixedUpdate();

                    timer += Time.deltaTime;
                    if (timer > atkTimer)
                    {
                        isAtk = true;
                        ballisticAngle = Random.Range(minBallisticAngle, maxBallisticAngle);
                    }
                }

            }
        }
    }
    /// <summary>
    /// 将相距最近的 敌人设置为目标
    /// </summary>
    private void SetEnemy()
    {
        if (GameManager.Instance.players.Length > 1)
        {
            shortestDistance = 9999;
            for (int i = 0; i < GameManager.Instance.players.Length; i++)
            {
                if (GameManager.Instance.players[i].transform != playerPoint)
                {
                    float distance = Vector3.Distance(GameManager.Instance.players[i].transform.position, transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        enemyPoint = GameManager.Instance.players[i].transform;
                    }
                }
            }
        }
        else
        {
            enemyPoint = null;
        }
    }
}
