using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    public Transform lookingDir;

    //Variables used for steam devices
    //SteamVR_Controller.Device device;
    public SteamVR_TrackedObject controller;
    private Vector2 touchpad;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private float movementSpeed = 4.0f;
    private float sensitivityX = 1.1f;
    private Vector3 playerPos;
    private Vector3 newCameraPosition;


    //Velocity variables for movement
    private float initialVelocity = 0.0f;
    private float finalVelocity = 2.5f;
    private float currentVelocity = 0.0f;
    private float accelerationRate = 1.5f;
    private float decelerationRate = 10.0f;

    //Variables for rotation velocity
    private float initialRotationVelocity = 0.0f;
    private float finalRotationVelocity = 1.0f;
    private float currentRotationVelocity = 0.0f;
    private float rotationAccelerationRate = 2.0f;
    private float rotationDecelerationRate = 10.0f;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //device = SteamVR_Controller.Input((int)controller.index);
        int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        int leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);

        SteamVR_Controller.Device rightDevice = SteamVR_Controller.Input(rightIndex);
        SteamVR_Controller.Device leftDevice = SteamVR_Controller.Input(leftIndex);



        if (rightDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || Input.GetAxis("Horizontal") !=0)
        {
            Moving(rightDevice);
            //  transform.position -= lookingDir.transform.forward * Time.deltaTime * (touchpad.y * 0.5f);
        
        }
        else if (leftDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || Input.GetAxis("Horizontal") != 0)
        {
            Moving(leftDevice);
        }
        else
        {
            currentVelocity -= decelerationRate * Time.deltaTime;
            currentRotationVelocity -= (rotationDecelerationRate * Time.deltaTime);
        }

        //Doing the movement here after being clamped
        currentVelocity = Mathf.Clamp(currentVelocity, initialVelocity, finalVelocity);
        transform.position += lookingDir.transform.forward * Time.deltaTime * currentVelocity * touchpad.y;
        currentRotationVelocity = Mathf.Clamp(currentRotationVelocity, initialRotationVelocity, finalRotationVelocity);
        transform.Rotate(0, currentRotationVelocity * touchpad.x, 0);
    }


    private void Moving(SteamVR_Controller.Device device)
    {
        //Reading the values from touchpad
        touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

        //Move based on the y input of the touchpad
        if (touchpad.y > 0.2f || touchpad.y < -0.2f)
        {
            currentVelocity += (accelerationRate * Time.deltaTime);
        }

        //Rotation
            //Rotate based on x input of the touchpad
            if (touchpad.x > 0.6f || touchpad.x < -0.6f)
            {
                currentRotationVelocity += (rotationAccelerationRate * Time.deltaTime);
            }
        }
    }
