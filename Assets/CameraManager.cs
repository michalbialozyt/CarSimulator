using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    public float distance = 5f;
    public float height = 2f;
    public float dampening = 1f;
    public float h2 = 0f;
    public float d2 = 0f;
    public float l = 0f;
    private int camMode = 0;

    // The settings for the third camera view
    public float thirdCamDistance = 6f; // A bit behind the vehicle
    public float thirdCamHeight = 1.5f; // A bit lower than the default height
    public float thirdCamDampening = 0.1f; // Less dynamic means higher dampening for smoother follow

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camMode = (camMode + 1) % 3; // Now cycles through three camera modes
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        switch (camMode)
        {
            case 2:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0, thirdCamHeight, d2 - thirdCamDistance + 1));
                transform.rotation = Quaternion.LookRotation(focus.transform.position - transform.position + 0.1f * Vector3.down * height);
                break;
            case 1:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(l, h2, d2));
                transform.rotation = focus.transform.rotation;
                Camera.main.fieldOfView = 90f;
                break;
            case 0:
                transform.position = Vector3.Lerp(transform.position, focus.transform.position + focus.transform.TransformDirection(new Vector3(0f, height, -distance)), dampening * Time.deltaTime);
                transform.LookAt(focus.transform);
                break;
        }

        Debug.Log(camMode);
    }

    public void SetFocus(GameObject newFocus)
    {
        focus = newFocus;
    }

    public void UpdateCameraSettings(float newDistance, float newHeight, float newDampening)
    {
        distance = newDistance;
        height = newHeight;
        dampening = newDampening;
    }
}
