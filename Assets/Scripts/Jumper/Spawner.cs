using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool entered = false;
    void Start(){

    }

private void OnTriggerEnter(Collider other)
    {
        if (entered) return;
        if (other.tag != "Player") return;
        if (!GameManager.Instance) return;

        entered = true;
        int platforms = GameManager.Instance.PlatformsCleared++;

        GameObject randomPrefab = GameManager.Instance.Prefabs[Random.Range(0, GameManager.Instance.Prefabs.Length)];

        Vector3 pos = new Vector3(transform.position.x + Random.Range(-7.0f, 7.0f), transform.position.y + Random.Range(1, 6.0f), transform.position.z + Random.Range(7.0f, 13.0f));

        GameObject go = Instantiate(randomPrefab, pos, Quaternion.identity, GameManager.Instance.transform);

        GameManager.Instance.Instances.Add(go);
        CameraController cam = FindObjectOfType<CameraController>();

        cam.nexttarget = go.transform;


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
