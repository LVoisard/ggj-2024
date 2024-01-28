using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public static TrackManager Instance { get; private set; }

    [SerializeField] private EndlessRunnerTrack startingTrack;

    [SerializeField] private List<EndlessRunnerTrack> tracksPrefabs = new List<EndlessRunnerTrack>();

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Car car;

    public int score = -1;

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
        RestartGame();
        //SpawnNextTrack(startingTrack);
    }

    public void SpawnNextTrack(EndlessRunnerTrack latest)
    { 
        int trackId = Random.Range(0, tracksPrefabs.Count);
        score++;
        scoreText.text = $"Score: {score}";
        car.IncrementMaxSpeed(score);

        EndlessRunnerTrack track = Instantiate(tracksPrefabs[trackId], transform);
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

    public async void RestartGame()
    {
        print("restarted Game");

       
        

        foreach (EndlessRunnerTrack t in spawnedTracksList)
        {
            Destroy(t.gameObject);
        }
        spawnedTracksList.Clear();

        EndlessRunnerTrack track = Instantiate(startingTrack, Vector3.zero, Quaternion.identity, transform);
        spawnedTracksList.Add(track);
        car.transform.position = Vector3.zero;
        car.transform.rotation = Quaternion.identity;


        score = 0;
        scoreText.text = $"Score: {score}";
        car.stopCar = true;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }

        car.stopCar = false;

        print("Finished");

    }
}