using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ！
/// </summary>
public class WeaponController : MonoBehaviour
{
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
    [Tooltip("飞行速度")] public float moveSpeed;
    /// <summary>
    /// 目标点
    /// </summary>
    Vector3 targetDirection;

    public GameObject targetgameObject;

    [SerializeField] float lowSpeed = 8f;
    /// <summary>
    /// 高速
    /// </summary>
    [SerializeField] float highSpeed = 25f;
    /// <summary>
    /// 低速转高速的延迟时间
    /// </summary>
    [SerializeField] float variableSpeedDelay = 0.5f;


    private void Start()
    {
        StartCoroutine(HomingCoroutine(targetgameObject));
        StartCoroutine(nameof(VariableSpeedCoroutine));

    }
    /// <returns></returns>
    public IEnumerator HomingCoroutine(GameObject target)
    {
        ballisticAngle = Random.Range(minBallisticAngle, maxBallisticAngle);
        while (gameObject.activeSelf)
        {
            if (target.activeSelf)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > 5)
                {
                    //目标方向等于目标位置减去自身位置
                    targetDirection = target.transform.position - transform.position;
                    //获取旋转角度  原点距离目标点的夹角 返回一个弧度   将弧度转换为角度
                    var angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
                    //设置子弹的旋转角度(角度,围绕旋转的轴)
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                    // 叠加旋转
                    transform.rotation *= Quaternion.Euler(0f, ballisticAngle, 0f);
                    //移动子弹
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                }
                else
                {
                    targetDirection = target.transform.position - transform.position;
                    var angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.up);
                    transform.RotateAround(target.transform.position, Vector3.up, -2);
                }

            }
            else
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            // projectile.Move();
            yield return null;
        }

    }
    /// <summary>
    /// 变速协程
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    IEnumerator VariableSpeedCoroutine()
    {
        moveSpeed = lowSpeed;
        yield return new WaitForSeconds(variableSpeedDelay);
        moveSpeed = highSpeed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }
}
