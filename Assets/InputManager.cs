using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_throttle;
    public float m_steer;

    // Update is called once per frame
    void Update()
    {
        m_throttle = Input.GetAxis("Vertical");
        m_steer = Input.GetAxis("Horizontal");
    }
}
