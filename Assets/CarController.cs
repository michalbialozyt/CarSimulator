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
    void Start()
    {
        input = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        if (CM) { 
            rb.centerOfMass = CM.position;
        }
    }

    void Update()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = 10*strengthCoefficient * Time.deltaTime * input.throttle;
        }

        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * input.steer;
            wheel.transform.localEulerAngles = new Vector3(0f,input.steer*maxTurn,0f);
        }

        foreach (GameObject mesh in meshes)
        {
            float signedSpeed = Vector3.Dot(rb.velocity, transform.forward);
            
            mesh.transform.Rotate(signedSpeed / (2 * Mathf.PI * 0.3f), 0f, 0f);
            //Debug.Log("Current Velocity: " + signedSpeed);
        }
    }
}
