using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class CarController : MonoBehaviour
{
    public InputManager m_inputManager;
    public List<WheelCollider> m_throttleWheels;
    public List<WheelCollider> m_steeringWheels;
    public float m_strenghtCoeff = 20000f;
    public float m_maxTurn = 20f;

    // Update is called once per frame
    void Start()
    {
        m_inputManager = GetComponent<InputManager>();
    }
    void FixedUpdate()
    {
        foreach(WheelCollider wheel in m_throttleWheels)
        {
            wheel.motorTorque = m_strenghtCoeff * Time.deltaTime * m_inputManager.m_throttle;
        }

        foreach (WheelCollider wheel in m_steeringWheels)
        {
            wheel.steerAngle = m_maxTurn * m_inputManager.m_steer;
        }
    }
}
