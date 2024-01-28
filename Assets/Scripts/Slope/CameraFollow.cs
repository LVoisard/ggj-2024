using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float LerpFactor = 0.2f;
    public Vector3 CameraOffset;

    private bool HasValidTarget;

    void Start()
    {
        HasValidTarget = Target != null;
    }

    void FixedUpdate()
    {
        if (!HasValidTarget) return;

        Vector3 newPos = Vector3.Lerp(transform.position, Target.transform.position + CameraOffset, Mathf.Clamp01(LerpFactor));
        transform.position = newPos;
        transform.LookAt(Target);
    }
}
