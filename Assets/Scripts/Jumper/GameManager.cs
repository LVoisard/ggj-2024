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

        GameObject startingInstance = Instantiate(StartingPlatformPrefab, new Vector3(0, 0,0), Quaternion.identity);
        Instances.Add(startingInstance);
        Movement mov = FindObjectOfType<Movement>();
        mov.transform.position = startingInstance.transform.position + new Vector3(0, 5f, 0);
    }

    private void DestroyAllPlatforms()
    {
        if (Instances != null)
        {
            foreach (GameObject go in Instances) 
            {
                Destroy(go);
            }
            Instances.Clear();
        }
    }
    public GameObject[] Prefabs;
    public List<GameObject> Instances = new List<GameObject>();
    public GameObject StartingPlatformPrefab;

    public int PlatformsCleared = 0;

    private void Start()
    {
        SetupNewGame();
    }
}
