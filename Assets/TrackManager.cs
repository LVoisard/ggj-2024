using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public static TrackManager Instance { get; private set; }

    [SerializeField] private EndlessRunnerTrack startingTrack;

    [SerializeField] private List<EndlessRunnerTrack> tracksPrefabs = new List<EndlessRunnerTrack>();

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Car car;

    int score = -1;

    private List<EndlessRunnerTrack> spawnedTracksList = new List<EndlessRunnerTrack>();

    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        } 
        else if (Instance != this) 
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnedTracksList.Add(startingTrack);
        //SpawnNextTrack(startingTrack);
    }

    public void SpawnNextTrack(EndlessRunnerTrack latest)
    { 
        int trackId = Random.Range(0, tracksPrefabs.Count);
        score++;
        scoreText.text = $"Score: {score}";
        car.IncrementMaxSpeed(score);

        EndlessRunnerTrack track = Instantiate(tracksPrefabs[trackId]);
        track.transform.rotation = latest.nextTrackPosition.rotation;
        Vector3 offset = track.transform.position - track.startTrackPosition.position;
        track.transform.position = latest.nextTrackPosition.position + offset;


        spawnedTracksList.Add(track);
        if (spawnedTracksList.Count > 3) 
        {
            EndlessRunnerTrack t = spawnedTracksList[0];
            spawnedTracksList.RemoveAt(0);
            Destroy(t.gameObject);
        }
    }
}
