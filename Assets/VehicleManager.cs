using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public GameObject car;
    public GameObject truck;
    public CameraManager cameraManager; // Reference to your CameraManager
    private GameObject currentVehicle;

    // Camera settings for the car
    public float carDistance = 5f;
    public float carHeight = 2f;
    public float carDampening = 1f;

    // Camera settings for the truck
    public float truckDistance = 7f;
    public float truckHeight = 3f;
    public float truckDampening = 1f;

    void Start()
    {
        // Start with the car as the current vehicle
        currentVehicle = car;
        car.SetActive(true);
        truck.SetActive(false);

        // Update the camera focus and settings
        if (cameraManager != null)
        {
            cameraManager.SetFocus(currentVehicle);
            cameraManager.UpdateCameraSettings(carDistance, carHeight, carDampening);
        }
    }

    void Update()
    {
        // Check for key press to switch vehicles (e.g., 'V' key)
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchVehicle();
        }
    }

    void SwitchVehicle()
    {
        if (currentVehicle == car)
        {
            currentVehicle.SetActive(false);
            currentVehicle = truck;
            currentVehicle.SetActive(true);

            // Update camera settings for the truck
            if (cameraManager != null)
            {
                cameraManager.SetFocus(currentVehicle);
                cameraManager.UpdateCameraSettings(truckDistance, truckHeight, truckDampening);
            }
        }
        else
        {
            currentVehicle.SetActive(false);
            currentVehicle = car;
            currentVehicle.SetActive(true);

            // Update camera settings for the car
            if (cameraManager != null)
            {
                cameraManager.SetFocus(currentVehicle);
                cameraManager.UpdateCameraSettings(carDistance, carHeight, carDampening);
            }
        }
    }
}
