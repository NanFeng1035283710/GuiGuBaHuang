using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 轨道自动旋转
/// </summary>
public class TrackAutoRotate : MonoBehaviour
{

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 4);
    }
  

}
