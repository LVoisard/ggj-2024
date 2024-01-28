using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float ForwardMoveSpeed;
    public float LateralMoveSpeed;
    public float PlatformSpeedUp;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetVelocityZ(ForwardMoveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnInputMove(KeyCode.A, Vector3.left * LateralMoveSpeed);
        OnInputMove(KeyCode.D, Vector3.right * LateralMoveSpeed);

        SetVelocityZ(GetSpeedValue(SlopeGameManager.Instance.PlatformsCleared));
        CheckForDeath();
    }

    void OnInputMove(KeyCode code, Vector3 force)
    {
        if (Input.GetKey(code))
        {
            rb.AddForce(force);
        }
    }

    public void SetVelocityZ(float z) => rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, z);

    float GetSpeedValue(float platforms)
    {
        return platforms == 0 ? PlatformSpeedUp : (2 + 6*Mathf.Log(2*platforms) * PlatformSpeedUp);
    }

    void CheckForDeath()
    {
        GameObject building = null;

        if (SlopeGameManager.Instance.Instances.TryPeek(out building))
            if (building && building.transform)
                if (SlopeGameManager.Instance.Instances.Count <= 1 && transform.position.y < (building.transform.position.y - 50))
                    Die();
    }

    void Die()
    {
        transform.position = Vector3.zero;
        SlopeGameManager.Instance.SetupNewGame();
    }
}
