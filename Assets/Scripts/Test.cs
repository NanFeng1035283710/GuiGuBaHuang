using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// ！
    /// </summary>
public class Test : MonoBehaviour
{
    //private void OnParticleCollision(GameObject other)
    //{
    //  //  Debug.Log(other);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

}
