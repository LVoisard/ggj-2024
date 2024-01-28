using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlizzyPlayer : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform[] PlayerPos;

    public float BulletSpeed;

    public float BulletInterval;
    private float ShootTimer = 0f;

    public float ForwardMoveSpeed;
    public float LateralMoveSpeed;

    void Update()
    {
        Shoot();
    }

    void FixedUpdate()
    {
        OnInputMove(KeyCode.A, Vector3.left * LateralMoveSpeed * Time.fixedDeltaTime);
        OnInputMove(KeyCode.D, Vector3.right * LateralMoveSpeed * Time.fixedDeltaTime);

        transform.Translate(Vector3.forward * ForwardMoveSpeed * Time.fixedDeltaTime);

        foreach (var pos in PlayerPos)
        {
            if (pos.position.y < -5)
            {
                pos.position = new Vector3(pos.position.x, 0, pos.position.z);
                pos.gameObject.SetActive(false);
            }
        }
    }

    void OnInputMove(KeyCode code, Vector3 force)
    {
        if (Input.GetKey(code))
        {
            transform.Translate(force);
        }
    }

    void Shoot()
    {
        ShootTimer += Time.deltaTime;
        if (ShootTimer < BulletInterval) return;
        ShootTimer = 0f;

        foreach (var pos in PlayerPos)
        {
            if (!pos.gameObject.activeInHierarchy) continue;

            GameObject bullet = Instantiate(BulletPrefab, pos.position + Vector3.forward, Quaternion.identity);
            GlizzyBullet gb = bullet.GetComponent<GlizzyBullet>();
            gb.Speed = BulletSpeed;
            Destroy(bullet, 1f);
        }
    }

    public void AddCrewMember()
    {
        foreach (var pos in PlayerPos)
        {
            if (!pos.gameObject.activeInHierarchy)
            {
                pos.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void IncrementGunSpeed()
    {
        if (BulletInterval > 0.1f)
            BulletInterval -= 0.05f;
    }
}
