using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class CarController : MonoBehaviour
{
    public InputManager input;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public List<GameObject> meshes;
    public float strengthCoefficient = 20000f;
    public float maxTurn = 20f;
    public Rigidbody rb;
    public Transform CM;
    public bool isTruck = false; // Flag to identify if the vehicle is a truck

    void Start()
    {
        input = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        if (CM)
        {
            rb.centerOfMass = CM.position;
        }

        SetupWheels(throttleWheels);
        SetupWheels(steeringWheels);
    }

    void SetupWheels(List<WheelCollider> wheels)
    {
        foreach (WheelCollider wheel in wheels)
        {
            var suspension = wheel.suspensionSpring;
            suspension.spring = 8000;  // Suspension spring force
            suspension.damper = 1100;   // Damping force
            suspension.targetPosition = 1f;  // Neutral position of the spring
            wheel.suspensionSpring = suspension;
            wheel.suspensionDistance = 0.5f; // Suspension travel distance
        }
    }

    void Update()
    {
        ApplyThrottle();
        ApplySteering();
        UpdateWheelMeshes();
    }

    void ApplyThrottle()
    {
        float throttleInput = input.throttle;

        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = 10 * strengthCoefficient * Time.deltaTime * throttleInput;
        }
    }

    void ApplySteering()
    {
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * input.steer;
        }
    }

    void UpdateWheelMeshes()
    {
        for (int i = 0; i < throttleWheels.Count; i++)
        {
            UpdateWheelMesh(throttleWheels[i], meshes[i]);
        }

        for (int i = 0; i < steeringWheels.Count; i++)
        {
            UpdateWheelMesh(steeringWheels[i], meshes[i + throttleWheels.Count]);
        }
    }

    void UpdateWheelMesh(WheelCollider collider, GameObject mesh)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        mesh.transform.position = position;
        mesh.transform.rotation = rotation;
    }
}
