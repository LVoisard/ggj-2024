using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlizzyGameManager : MonoBehaviour
{
    private static GlizzyGameManager _instance;
    public static GlizzyGameManager Instance { get { return _instance; } }

    public int ObstacleNum;
    public int TrackLength;
    public int StartOffset;

    public GameObject[] Obstacles;
    public GlizzyPlayer Player;
    public Transform ObstacleHolder;

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

    void Start()
    {
        StartGame();
    }

    internal void StartGame()
    {
        Cleanup();
        Player.transform.position = Vector3.zero;
        Player.BulletInterval = 0.5f;

        for (int i = 0; i < Player.transform.childCount; i++)
        {
            var go = Player.transform.GetChild(i).gameObject;
            if (go.tag != "MainCamera")
                go.transform.position = new Vector3(go.transform.position.x, 0, go.transform.position.z);
            go.SetActive(go.tag != "Pawn");
        }
        int division = ((TrackLength - 30) / ObstacleNum);
        for (int i = 0; i < ObstacleNum; i++)
        {
            GlizzyObstacle obs = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], new Vector3(Random.Range(-3f, 3f), 0.5f, StartOffset + i * division), Quaternion.identity, ObstacleHolder).GetComponent<GlizzyObstacle>();
            obs.Score = Random.Range((i + 1) * 3, (i + 1) * 5);
        }
    }


    private void OnDestroy()
    {
        Cleanup();
    }

    void Cleanup()
    {
        foreach (Transform transform in ObstacleHolder.transform)
        {
            Destroy(transform.gameObject);
        }
    }
}
