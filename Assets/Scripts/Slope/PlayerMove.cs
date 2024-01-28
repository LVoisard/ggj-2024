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
        return platforms == 0 ? PlatformSpeedUp : (8 + 4*Mathf.Log(4*platforms) * PlatformSpeedUp);
    }
}
