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

    }
}
