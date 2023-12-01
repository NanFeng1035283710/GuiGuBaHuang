using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.ParticleSystem;

/// <summary>
/// 粒子子弹
/// </summary>
public class ProjectilesWeapon : MonoBehaviour
{
    public ParticleSystem[] ParticleSystems;
    public int patternIndex = 0;
    public float fireRate = 0.3f;
    public WaitForSeconds waitFor;
    public Transform enemyPoint;

    private CollisionModule collisionMode;
    /// <summary>
    /// 最短距离
    /// </summary>
    private float shortestDistance;
    private void Start()
    {
        ChoicePattern();
    }

    /// <summary>
    /// 选择子弹的模式
    /// 0.随机模式
    /// 1.追踪模式
    /// 2.同步散射
    /// 3.连续散射
    /// 4.弧形弹道
    /// 5.飞剑导弹
    /// </summary>
    private void ChoicePattern()
    {
        switch (patternIndex)
        {
            case 0:
                StartCoroutine(nameof(RandomModeCoroutine));
                break;
            case 1:
                StartCoroutine(nameof(TraceModeCoroutine));
                break;
            case 2:
                StartCoroutine(nameof(SynchronizationScatteringCroutine));
                break;
            case 3:
                StartCoroutine(nameof(RotateScatteringCroutine));
                break;
            case 4:
                StartCoroutine(nameof(ArcTrajectoryCroutine));
                break;
            case 5:
                StartCoroutine(nameof(FlyingSwordCroutine));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 随机模式.朝随机方向发射子弹
    /// </summary>
    /// <returns></returns>
    private IEnumerator RandomModeCoroutine()
    {
        EnableVelocityOverLifetimeModule(true);
        while (true)
        {
            transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }
    /// <summary>
    ///  追踪相距最近的敌人连发子弹
    /// </summary>
    /// <returns></returns>
    private IEnumerator TraceModeCoroutine()
    {
        EnableVelocityOverLifetimeModule(false);
        while (true)
        {
            SetEnemy();
            if (enemyPoint != null)
            {
                transform.LookAt(enemyPoint.position);
                for (int i = 0; i < 5; i++)
                {
                    Fire();
                    yield return new WaitForSeconds(0.3f);
                }
            }
            yield return new WaitForSeconds(fireRate * 5);
        }
    }
    /// <summary>
    /// 同时朝四周发射
    /// </summary>
    /// <returns></returns>
    private IEnumerator SynchronizationScatteringCroutine()
    {
        EnableVelocityOverLifetimeModule(false);
        while (true)
        {
            for (int i = 0; i < 9; i++)
            {
                transform.rotation = Quaternion.Euler(0, 40 * i, 0);
                Fire();
            }
            yield return new WaitForSeconds(fireRate * 5);
        }
    }
    /// <summary>
    /// 旋转朝四周发射
    /// </summary>
    /// <returns></returns>
    private IEnumerator RotateScatteringCroutine()
    {
        EnableVelocityOverLifetimeModule(false);
        while (true)
        {
            for (int i = 0; i < 20; i++)
            {
                transform.rotation = Quaternion.Euler(0, 360 / 20 * i, 0);
                Fire();
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(fireRate * 5);
        }
    }
    /// <summary>
    /// 追踪相距最近的敌人连发弧形弹道子弹
    /// </summary>
    /// <returns></returns>
    private IEnumerator ArcTrajectoryCroutine()
    {
        EnableVelocityOverLifetimeModule(true);
        while (true)
        {
            SetEnemy();
            if (enemyPoint != null)
            {
                transform.LookAt(enemyPoint.position);
                for (int i = 0; i < 5; i++)
                {
                    Fire();
                    yield return new WaitForSeconds(0.3f);
                }
            }
            yield return new WaitForSeconds(fireRate * 5);
        }
    }
    /// <summary>
    /// 飞剑
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlyingSwordCroutine()
    {
        yield return null;
    }
    /// <summary>
    /// 开火
    /// </summary>ssssssssss
    private void Fire()
    {
        foreach (var ps in ParticleSystems)
            ps.Emit(1);
    }
    /// <summary>
    /// 开启关闭粒子系统的VelocityOverLifetimeModule模块
    /// </summary>
    /// <param name="isEnable">是否开启</param>
    private void EnableVelocityOverLifetimeModule(bool isEnable)
    {
        foreach (var ps in ParticleSystems)
        {
            ParticleSystem.VelocityOverLifetimeModule velocityMode = ps.velocityOverLifetime;
            velocityMode.enabled = isEnable;
        }
    }
    public void SetParticleCollisionLayer(int id)
    {
        collisionMode = ParticleSystems[0].collision;
        //设置粒子碰撞层级.除自身外的所有层
        collisionMode.collidesWith = ~(1 << id);
    }
    /// <summary>
    /// 设置相距最近的的敌人为目标
    /// </summary>
    private void SetEnemy()
    {
        if (GameManager.Instance.players.Length > 1)
        {
            shortestDistance = 9999;
            for (int i = 0; i < GameManager.Instance.players.Length; i++)
            {
                if (GameManager.Instance.players[i] != transform.parent.gameObject)
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
