using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeGameManager : MonoBehaviour
{
    private static SlopeGameManager _instance;
    public static SlopeGameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject[] Prefabs;
    public GameObject[] Buildings;

    public Queue<GameObject> Instances;
    public GameObject StartingPlatform;

    public int PlatformsCleared = 0;

    private void Start()
    {
        Instances = new Queue<GameObject>();
        Instances.Enqueue(StartingPlatform);
    }
}
