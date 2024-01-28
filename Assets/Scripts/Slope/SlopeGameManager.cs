using System;
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

    public TMPro.TMP_Text Text;

    public int PlatformsCleared = 0;

    private void Start()
    {
        SetupNewGame();
    }

    private void Update()
    {
        Text.text = "SCORE: " + PlatformsCleared;
    }

    public void SetupNewGame()
    {
        DestroyAllPlatforms();

        PlatformsCleared = 0;
        Instances = new Queue<GameObject>();

        GameObject startingInstance = Instantiate(StartingPlatform, new Vector3(0, -4, 10), Quaternion.identity);
        Instances.Enqueue(startingInstance);
    }

    private void OnDestroy()
    {
        DestroyAllPlatforms();
    }

    private void DestroyAllPlatforms()
    {
        if (Instances != null)
        {
            while (Instances.Count != 0)
            {
                GameObject i = Instances.Dequeue();
                if (i)
                    Destroy(i);
            }
        }
    }
}
