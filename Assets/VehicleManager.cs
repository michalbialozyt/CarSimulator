using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public GameObject car;
    public GameObject truck;
    public CameraManager cameraManager; // Reference to your CameraManager
    private GameObject currentVehicle;
    private CarController carController;

    // Camera settings for the car
    public float carDistance = 5f;
    public float carHeight = 2f;
    public float carDampening = 1f;

    // Camera settings for the truck
    public float truckDistance = 5f;
    public float truckHeight = 3f;
    public float truckDampening = 1f;

    void Start()
    {
        // Start with the car as the current vehicle
        currentVehicle = car;
        car.SetActive(true);
        truck.SetActive(false);
        cameraManager.truckOffset = new Vector3(-2f, 1f, 1f);

        carController = currentVehicle.GetComponent<CarController>();
        if (carController != null)
        {
            carController.isTruck = false;
        }

        // Update the camera focus and settings
        if (cameraManager != null)
        {
            cameraManager.SetFocus(currentVehicle, carDistance, carHeight, carDampening, false);
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
            truck.SetActive(true);
            currentVehicle = truck;

            carController = currentVehicle.GetComponent<CarController>();
            if (carController != null)
            {
                carController.isTruck = true;
            }

            // Update camera settings for the truck
            if (cameraManager != null)
            {
                cameraManager.SetFocus(currentVehicle, truckDistance, truckHeight, truckDampening, true);
            }
        }
        else
        {
            currentVehicle.SetActive(false);
            car.SetActive(true);
            currentVehicle = car;

            carController = currentVehicle.GetComponent<CarController>();
            if (carController != null)
            {
                carController.isTruck = false;
            }

            // Update camera settings for the car
            if (cameraManager != null)
            {
                cameraManager.SetFocus(currentVehicle, carDistance, carHeight, carDampening, false);
            }
        }
    }
}
