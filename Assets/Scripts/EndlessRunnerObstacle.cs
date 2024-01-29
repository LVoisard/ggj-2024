using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRunnerObstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        TrackManager.Instance.RestartGame();
    }
}
