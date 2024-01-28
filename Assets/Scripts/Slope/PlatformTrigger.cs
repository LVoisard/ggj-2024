using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if (!SlopeGameManager.Instance) return;

        int platforms = SlopeGameManager.Instance.PlatformsCleared++;

        GameObject randomPrefab = SlopeGameManager.Instance.Prefabs[Random.Range(0, SlopeGameManager.Instance.Prefabs.Length)];

        Vector3 pos = new Vector3(0, transform.position.y - (15 + platforms), transform.position.z + (24+ 1.2f* platforms));

        SlopeGameManager.Instance.Instances.Enqueue(Instantiate(randomPrefab, pos, Quaternion.identity, SlopeGameManager.Instance.transform));
        Instantiate(SlopeGameManager.Instance.Buildings[0], pos, Quaternion.identity, transform.parent.parent);
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke(nameof(Cleanup), 8f);
    }

    void Cleanup()
    {
        Destroy(SlopeGameManager.Instance.Instances.Dequeue());
    }
}
