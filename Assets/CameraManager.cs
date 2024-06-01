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

    // Manual offset adjustments for the truck
    public Vector3 truckOffset = Vector3.zero;
    public bool isTruck = false;

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
        Vector3 offset = Vector3.zero;

        if (isTruck) // If the current vehicle is a truck
        {
            offset = truckOffset;
        }

        switch (camMode)
        {
            case 2:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(0, thirdCamHeight, -thirdCamDistance)) + offset;
                transform.rotation = Quaternion.LookRotation(focus.transform.position - transform.position);
                break;
            case 1:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(l, h2, d2)) + offset;
                transform.rotation = focus.transform.rotation;
                Camera.main.fieldOfView = 90f;
                break;
            case 0:
                transform.position = Vector3.Lerp(transform.position, focus.transform.position + focus.transform.TransformDirection(new Vector3(0f, height, -distance)) + offset, dampening * Time.deltaTime);
                transform.LookAt(focus.transform.position + (isTruck ? focus.transform.TransformDirection(Vector3.forward) : focus.transform.TransformDirection(Vector3.back)));
                break;
        }

        Debug.Log(camMode);
    }

    public void SetFocus(GameObject newFocus, float newDistance, float newHeight, float newDampening, bool isTruck)
    {
        focus = newFocus;
        this.isTruck = isTruck;
        UpdateCameraSettings(newDistance, newHeight, newDampening);
    }

    public void UpdateCameraSettings(float newDistance, float newHeight, float newDampening)
    {
        distance = newDistance;
        height = newHeight;
        dampening = newDampening;
    }
}
