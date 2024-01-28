using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackType
{ 
    Straight,
    Corner
}

public class EndlessRunnerTrack : MonoBehaviour
{
    private bool entered = false;
    public TrackType trackType;
    public Transform nextTrackPosition;
    public Transform startTrackPosition;
    public Vector3 size;


    private void OnTriggerEnter(Collider other)
    {
        if(!entered)
            TrackManager.Instance.SpawnNextTrack(this);
        
        entered = true;
    }
}
