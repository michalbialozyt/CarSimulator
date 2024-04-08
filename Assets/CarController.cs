using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public InputManager m_inputManager;
    public List<WheelCollider> m_throttleWheels;
    public List<GameObject> m_steeringWheels;
    public List<GameObject> meshes;
    public float m_strenghtCoeff = 10000f;
    public float m_maxTurn = 20f;
    public float m_brakeStrength = 20f;
    public Transform CM;
    public Rigidbody rb;

    // Update is called once per frame
    void Start()
    {
        m_inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        if (CM)
        {
            rb.centerOfMass = CM.position;
        }
    }

    void FixedUpdate()
    {
        foreach(WheelCollider wheel in m_throttleWheels)
        {
            if (m_inputManager.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = m_brakeStrength * Time.deltaTime;
            }
            else
            {
                wheel.motorTorque = m_strenghtCoeff * Time.deltaTime * m_inputManager.m_throttle;
                wheel.brakeTorque = 0f;
            }
        }

        foreach (GameObject wheel in m_steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = m_maxTurn * m_inputManager.m_steer;
            wheel.transform.localEulerAngles = new Vector3(0f, m_inputManager.m_steer * m_maxTurn, 0f);
        }

        foreach (GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1)/ (2 * Mathf.PI * 0.33f),0f,0f);
        }
    }
}
