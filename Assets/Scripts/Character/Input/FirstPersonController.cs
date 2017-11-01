using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(FlashlightHandler))]
public class FirstPersonController : MonoBehaviour {

    private string horizontalAxisName = "Horizontal";
    private string forwardAxisName = "Vertical";
    private string leftShiftAxisName = "Left Shift";

    private Quaternion startCameraRotation;

    //Inspector Set Variables
    //
    [Header("Required Controller Components")]
    public CharacterController characterController;
    public Camera firstPersonCamera;
    public FlashlightHandler flashlight;

    [Header("Camera Settings")]
    [Range(0, 500)]
    public float mouseSensitivity;

    [Header("Movement Settings")]
    [Range(0, 20)]
    public float slowWalkSpeed;
    [Range(0, 20)]
    public float walkSpeed;
    [Range(0, 20)]
    public float runSpeed;
    //
    //

    

    private void Start()
    {
        //Captures the starting rotation from the camera to determine the angle between it's current position and its centre position
        startCameraRotation = firstPersonCamera.transform.rotation;
    }
    public void FixedUpdate()
    {
        UpdatePosition();
        UpdateCamera();
    }

    //This helper method handles the users input and updates their position accordingly
    private void UpdatePosition()
    {
        float horizontalMove;
        float forwardMove;

        if (Input.GetButton(horizontalAxisName))
            horizontalMove = Input.GetAxis(horizontalAxisName);
        else
            horizontalMove = 0;
        if (Input.GetButton(forwardAxisName))
            forwardMove = Input.GetAxis(forwardAxisName);
        else
            forwardMove = 0;

        Vector3 direction = ((forwardMove * transform.forward) + (horizontalMove * transform.right)).normalized;

        if ((horizontalMove != 0) || (forwardMove != 0))
        {
            //Checks whether player is sprinting or focussing their flashlight
            if (Input.GetButton(leftShiftAxisName))
            {
                if (!flashlight.focussing)
                    characterController.Move(direction * runSpeed * Time.fixedDeltaTime);
                else
                    characterController.Move(direction * slowWalkSpeed * Time.fixedDeltaTime);
            }
            else
            {
                if (!flashlight.focussing)
                    characterController.Move(direction * walkSpeed * Time.fixedDeltaTime);
                else
                    characterController.Move(direction * slowWalkSpeed * Time.fixedDeltaTime);
            }
        }
    }
    
    //This helper method handles the users mouse input , which rotates their gameObject or the camera based on the axis
    private void UpdateCamera()
    {
        //Updates the camera rotation on the y and z axis, but keeps the original x rotation to determine the angle between it's current position and its starting position
        startCameraRotation = Quaternion.Euler(startCameraRotation.eulerAngles.x, firstPersonCamera.transform.rotation.eulerAngles.y, firstPersonCamera.transform.rotation.eulerAngles.z);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Rotates the gameObject on the y axis by the mouseX input * sensitivity
        this.transform.Rotate(0, mouseX * mouseSensitivity * Time.fixedDeltaTime, 0);
        //Rotates the gameObject on the x axis by the mouseY input * sensitivity (negative mouseY value to make forward mouse movement rotate the camera up)
        firstPersonCamera.transform.Rotate(-mouseY * mouseSensitivity * Time.fixedDeltaTime, 0, 0);
       

        float angle = Quaternion.Angle(firstPersonCamera.transform.rotation, startCameraRotation);

        if ((angle > 90) && (firstPersonCamera.transform.localEulerAngles.x > 180))
        {
            firstPersonCamera.transform.rotation = Quaternion.Euler(-90, firstPersonCamera.transform.rotation.eulerAngles.y, firstPersonCamera.transform.rotation.eulerAngles.z);
        }
        else
        {
            if ((angle > 90) && (firstPersonCamera.transform.localEulerAngles.x > 0))
            {
                firstPersonCamera.transform.rotation = Quaternion.Euler(90, firstPersonCamera.transform.rotation.eulerAngles.y, firstPersonCamera.transform.rotation.eulerAngles.z);
            }
        }
   }
}
