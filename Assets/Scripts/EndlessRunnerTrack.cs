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

    [SerializeField] private GameObject[] obstacles;


    private void Start()
    {
        if (obstacles.Length == 0) return;

        Bounds bounds = GetComponent<BoxCollider>().bounds;

        int obstaclesAmt = Random.Range(1, 4 + TrackManager.Instance.score / 10);

        for (int i = 0; i < obstaclesAmt; i++)
        {
            Vector3 point = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                0,
                Random.Range(bounds.min.z, bounds.max.z)
            );

            Instantiate(obstacles[Random.Range(0, obstacles.Length)], point + transform.position, Quaternion.AngleAxis(Random.Range(0,180),Vector3.up), transform);
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!entered)
            TrackManager.Instance.SpawnNextTrack(this);
        
        entered = true;
    }
}
