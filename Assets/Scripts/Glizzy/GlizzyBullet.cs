using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlizzyBullet : MonoBehaviour
{
    public float Speed;

    void Update()
    {
        transform.Translate(Speed * Vector3.forward * Time.deltaTime);
    }
}
