using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float carSnappingForce = 10f;
    private float baseCarSnappingForce;
    [SerializeField] private float steeringSpeed = 2;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] public float maxSpeed = 5.0f;

    [SerializeField] private List<TrailRenderer> tireTracks = new List<TrailRenderer>();
    [SerializeField] private ParticleSystem[] smokeParticles;

    [SerializeField] private AudioSource driftSound;

    public float baseSpeed = 5.0f;

    private Vector3 velocity = Vector3.zero;
    private float rotationInput = 0;

    public bool stopCar = false;

    private void Start()
    {
        baseSpeed = maxSpeed;
        baseCarSnappingForce = carSnappingForce;
        IncrementMaxSpeed(1);
    }

    private void Update()
    {
        if (stopCar)
        {
            foreach (TrailRenderer tireTrack in tireTracks)
            {
                tireTrack.emitting = false;
            }

            foreach (ParticleSystem smoke in smokeParticles)
            {
                smoke.enableEmission = false;
            }

            return;
        }

        rotationInput = Input.GetAxis("Horizontal");

        float angleDif = Quaternion.Angle(Quaternion.Euler(transform.forward), Quaternion.Euler(velocity.normalized));
        float a = Mathf.SmoothStep(0, 1, angleDif);
        foreach (TrailRenderer tireTrack in tireTracks)
        {
            tireTrack.emitting = a > 0.25f;
        }

        foreach (ParticleSystem smoke in smokeParticles)
        {
            smoke.enableEmission = a > 0.25f;
        }

        driftSound.volume = Mathf.SmoothStep(0, 0.15f, a / 0.15f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the car left/right


        if (!stopCar)
        {
            transform.rotation *= Quaternion.AngleAxis(rotationInput * steeringSpeed, Vector3.up);
        }
        velocity += transform.forward * acceleration * carSnappingForce * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        // transform.position += velocity * Time.deltaTime;

        velocity = Vector3.Slerp(velocity, (velocity * 0.8f), Time.deltaTime);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        if (!stopCar)
        {
            transform.position += velocity * Time.deltaTime;
        }
    }

    public void IncrementMaxSpeed(float amt)
    {
        amt = Mathf.Max(1, amt);
        maxSpeed = baseSpeed + baseSpeed * Mathf.Log10(amt);
        carSnappingForce = baseCarSnappingForce + baseCarSnappingForce * Mathf.Log10(baseCarSnappingForce + amt);
    }
}
