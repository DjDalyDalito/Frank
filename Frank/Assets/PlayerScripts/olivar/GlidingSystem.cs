using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlidingSystem : MonoBehaviour
{
    [SerializeField] private float BaseSpeed = 30f;
    [SerializeField] private float MaxThrustSpeed = 400f;
    [SerializeField] private float MinThrustSpeed = 3f;
    [SerializeField] private float ThrustFactor = 80f;
    [SerializeField] private float DragFactor = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float tiltStrength = 500f;
    [SerializeField] private float AccelerationPercentage;
    [SerializeField] private float lowPercent = 0.1f, highPercent = 1f;
    private float CurrentThrustSpeed;
    private float TiltValue, LerpValue;

    private Transform CameraTransform;
    private Rigidbody rb;

    private void Start()
    {
        CameraTransform = Camera.main.transform.parent;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GlidingMovement();
    }

    private void Update()
    {
        ManageRotation();
    }

    private void GlidingMovement()
    {
        float pitchInRads = transform.eulerAngles.x * Mathf.Deg2Rad;
        float mappedPitch = Mathf.Sin(pitchInRads) * ThrustFactor; // kan vara - mathf.sin om spelaren är omvänd
        float offsetMappedPitch = Mathf.Cos(pitchInRads) * DragFactor;
        float pitchInDeg = transform.eulerAngles.x % 360;
        float accelerationPercentage = pitchInDeg >= 300f ? lowPercent : highPercent;
        Vector3 glidingForce = (Vector3.forward * CurrentThrustSpeed); // kan vara - vector3 om spelaren är omvänd

        CurrentThrustSpeed += mappedPitch * accelerationPercentage * Time.deltaTime;
        CurrentThrustSpeed = Mathf.Clamp(CurrentThrustSpeed, 0, MaxThrustSpeed);

        if (rb.velocity.magnitude > MinThrustSpeed)
        {
            rb.AddRelativeForce(glidingForce);
            rb.drag = Mathf.Clamp(offsetMappedPitch, 0.2f, DragFactor);
        }
        else
        {
            CurrentThrustSpeed = 0;
        }

    }

    private void ManageRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        TiltValue += mouseX * tiltStrength;

        if (mouseX == 0)
        {
            TiltValue = Mathf.Lerp(TiltValue, 0, LerpValue);
            LerpValue += Time.deltaTime;
        }
        else
        {
            LerpValue = 0;
        }

        Quaternion targetRotation = Quaternion.Euler(CameraTransform.eulerAngles.x, CameraTransform.eulerAngles.y, TiltValue);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}


