using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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
    public void SetupNewGame()
    {
        DestroyAllPlatforms();

        PlatformsCleared = 0;
        Instances = new List<GameObject>();

        GameObject startingInstance = Instantiate(StartingPlatform, new Vector3(0, 0,0), Quaternion.identity);
        Instances.Add(startingInstance);
    }
        private void DestroyAllPlatforms()
    {
        if (Instances != null)
        {
            int y=0;
            while (Instances.Count != 0)
            {
                GameObject i = Instances[y];
                if (i)
                    Destroy(i);
                    y++;
            }
        }
    }
    public GameObject[] Prefabs;
    public List<GameObject> Instances;
    public GameObject StartingPlatform;

    public int PlatformsCleared = 0;

    private void Start()
    {
        Instances = new List<GameObject>();
        Instances.Add(StartingPlatform);
    }
}
