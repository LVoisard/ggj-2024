using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform nexttarget;
    public Transform currenttarget;
    public float LerpFactor = 10.0f;

    private void Start(){
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0,0,0);
        if (nexttarget != null)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward, (nexttarget.position - transform.position).normalized), Time.deltaTime * 3f);
        else
            transform.forward = Vector3.forward;
    }
}
