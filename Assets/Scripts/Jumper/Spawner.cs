using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    void Start(){

    }

private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if (!GameManager.Instance) return;

        int platforms = GameManager.Instance.PlatformsCleared++;

        GameObject randomPrefab = GameManager.Instance.Prefabs[Random.Range(0, GameManager.Instance.Prefabs.Length)];

        Vector3 pos = new Vector3(transform.position.x + Random.Range(-7.0f, 7.0f), transform.position.y + Random.Range(1, 6.0f), transform.position.z + Random.Range(7.0f, 13.0f));

        GameManager.Instance.Instances.Add(Instantiate(randomPrefab, pos, Quaternion.identity));
    }

    private void OnTriggerExit(Collider other)
    {   
        Invoke(nameof(Cleanup), 3);
    }

    void Cleanup()
    {
        Destroy(GameManager.Instance.Instances[GameManager.Instance.PlatformsCleared-3]);
    }
}
