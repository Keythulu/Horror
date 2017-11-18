using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActor))]
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {

    //In case these ever changes
    string horizontalAxisName = "Horizontal";
    string forwardAxisName = "Vertical";
    string leftShiftAxisName = "Left Shift";
    string leftCtrlAxisName = "Left Ctrl";
    string jumpAxisName = "Jump";

    //GetComponent references
    PlayerActor playerActor;
    CharacterController characterController;
    Camera firstPersonCamera;
    FlashlightController flashlight;

    //Camera movement helper variables
    Quaternion startCameraRotation;   
    Vector3 standingCameraPosition;
    Vector3 crouchingCameraPosition;

    //Initial gravity setter
    Vector3 gravity = Vector3.zero;

    //Movement state check variables
    bool jump;
    float horizontalMove;
    float forwardMove;
    float currentMoveSpeed = 0;
    Vector3 direction;
    [HideInInspector] public bool crouching;

    //Temporary transform for scaling player capsule for Debug
    [SerializeField]
    private Transform capsuleGraphics;

    public void Awake()
    {
        playerActor = GetComponent<PlayerActor>();
        if (playerActor == null)
            throw new UnassignedReferenceException();
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            throw new UnassignedReferenceException();
        firstPersonCamera = GetComponentInChildren<Camera>();
        if (firstPersonCamera == null)
            throw new UnassignedReferenceException();
        flashlight = GetComponentInChildren<FlashlightController>();
        if (flashlight == null)
            throw new UnassignedReferenceException();
    }

    private void Start()
    {
        startCameraRotation = firstPersonCamera.transform.rotation;      
        firstPersonCamera.transform.position = new Vector3(transform.position.x + 0.14f, transform.position.y + 0.55f, transform.position.z);
    }

    public void FixedUpdate()
    {
        UpdatePosition();
        UpdateCamera();
    }

    private void UpdatePosition()
    {
        if (Input.GetButton(horizontalAxisName))
            horizontalMove = Input.GetAxis(horizontalAxisName);
        else
            horizontalMove = 0;
        if (Input.GetButton(forwardAxisName))
            forwardMove = Input.GetAxis(forwardAxisName);
        else
            forwardMove = 0;

        if (((Input.GetButton(leftCtrlAxisName)) || 
                (Physics.Raycast(transform.position + characterController.center + new Vector3(0, characterController.height/2, 0), transform.up, 0.5f)) ||
                (Physics.Raycast(transform.position + characterController.center + new Vector3(0, characterController.height / 2, 0) + transform.forward * characterController.radius, transform.up, 0.5f)) ||
                (Physics.Raycast(transform.position + characterController.center + new Vector3(0, characterController.height / 2, 0) + -transform.forward * characterController.radius, transform.up, 0.5f)) ||
                (Physics.Raycast(transform.position + characterController.center + new Vector3(0, characterController.height / 2, 0) + transform.right * characterController.radius, transform.up, 0.5f)) ||
                (Physics.Raycast(transform.position + characterController.center + new Vector3(0, characterController.height / 2, 0) + -transform.right * characterController.radius, transform.up, 0.5f))) &&
                (!jump))
            crouching = true;
        else
            crouching = false;

        if (!Physics.Raycast(transform.position + characterController.center - new Vector3(0, characterController.height / 2, 0), -transform.up, 0.1f))
        {
            jump = true;
            gravity += Physics.gravity * Time.deltaTime;
            float jumpEnterMoveSpeed = currentMoveSpeed;
        }
        else
        {
            jump = false;
            gravity = Vector3.zero;
            if ((Input.GetButtonDown(jumpAxisName)) && (!crouching))
            {
                gravity += new Vector3(0, playerActor.playerData.movementProperties.jumpForce, 0);
            }
               
        }

        CrouchCheck();

        if (!jump)
            direction = ((forwardMove * transform.forward) + (horizontalMove * transform.right)).normalized;
        else
            direction = Vector3.Lerp(direction, ((forwardMove * transform.forward) + (horizontalMove * transform.right)).normalized, 0.03f);
        if ((horizontalMove != 0) || (forwardMove != 0))
        {
            if (crouching)
            {
                currentMoveSpeed = playerActor.playerData.movementProperties.slowWalkSpeed;
            }
            else
            {
                //Checks whether player is sprinting or focussing their flashlight
                if (!jump)
                {
                    if (Input.GetButton(leftShiftAxisName))
                    {
                        if (!flashlight.focussed)
                        {
                            currentMoveSpeed = playerActor.playerData.movementProperties.sprintSpeed;
                        }
                        else
                        {
                            currentMoveSpeed = playerActor.playerData.movementProperties.slowWalkSpeed;
                        }
                    }
                    else
                    {
                        if (!flashlight.focussed)
                        {
                            currentMoveSpeed = playerActor.playerData.movementProperties.walkSpeed;
                        }
                        else
                        {
                            currentMoveSpeed = playerActor.playerData.movementProperties.slowWalkSpeed;
                        }
                    }
                }
            }
        }
        characterController.Move((direction)  * currentMoveSpeed* Time.deltaTime);
        characterController.Move(gravity * Time.deltaTime);
    }
    
    private void UpdateCamera()
    {
        //Updates the camera rotation on the y and z axis, but keeps the original x rotation to determine the angle between it's current position and its starting position
        startCameraRotation = Quaternion.Euler(startCameraRotation.eulerAngles.x, firstPersonCamera.transform.rotation.eulerAngles.y, firstPersonCamera.transform.rotation.eulerAngles.z);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (playerActor.playerData.inputProperties.mouseSensitivity == 0)
            throw new System.Exception("Mouse sensitivity is set 0. Camera movement will be disabled");

        //Gameobject rotation
        transform.Rotate(0, mouseX * playerActor.playerData.inputProperties.mouseSensitivity * Time.fixedDeltaTime, 0);
        //Camera rotation
        firstPersonCamera.transform.Rotate(-mouseY * playerActor.playerData.inputProperties.mouseSensitivity * Time.fixedDeltaTime, 0, 0);
       

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

    private void CrouchCheck()
    {
        //Sets camera position for crouch switching
        standingCameraPosition = new Vector3(transform.position.x + 0.14f, transform.position.y + 0.55f, transform.position.z);
        crouchingCameraPosition = new Vector3(transform.position.x + 0.14f, transform.position.y - 0.45f, transform.position.z);

        if (crouching)
        {
            //This needs to be reset to allow 
            characterController.stepOffset = 0.3f;
            //Temporary lines for visualisation of crouch
            if (capsuleGraphics.localScale.y > 0.5f)
                capsuleGraphics.localScale = Vector3.Lerp(capsuleGraphics.localScale, new Vector3(1, 0.5f, 1), 0.5f);
            else
            {
                if (capsuleGraphics.localScale.y <= 0.55f)
                    capsuleGraphics.localScale = new Vector3(1, 0.5f, 1);
            }
            if (capsuleGraphics.localPosition.y > -0.5)
                capsuleGraphics.localPosition = Vector3.Lerp(capsuleGraphics.localPosition, new Vector3(0, -0.5f, 0), 0.5f);
            else
            {
                if (capsuleGraphics.localPosition.y <= -0.45f)
                    capsuleGraphics.localPosition = new Vector3(0, -0.5f, 0);
            }
            //
            if (characterController.height > 1)
                characterController.height = Mathf.Lerp(characterController.height, 1, 0.5f);
            else
            {
                if (characterController.height <= 1.1f)
                    characterController.height = 1;
            }
            if (characterController.center.y > -0.5)
                characterController.center = Vector3.Lerp(characterController.center, new Vector3(0, -0.5f, 0), 0.5f);
            else
            {
                if (characterController.center.y <= -0.45f)
                    characterController.center = new Vector3(0, -0.5f, 0);
            }
            if (Vector3.Distance(firstPersonCamera.transform.position, crouchingCameraPosition) > 0.5f)
            {
                firstPersonCamera.transform.position = Vector3.Lerp(firstPersonCamera.transform.position, crouchingCameraPosition, 0.5f);
            }
            else
            {
                if (Vector3.Distance(firstPersonCamera.transform.position, crouchingCameraPosition) < 0.5f)
                    firstPersonCamera.transform.position = crouchingCameraPosition;
            }
        }
        else
        {
            //This removes jumpineess when jumping over obstacles
            characterController.stepOffset = 1;
            //Temporary lines for visualisation of crouch
            if (capsuleGraphics.localScale.y < 1)
                capsuleGraphics.localScale = Vector3.Lerp(capsuleGraphics.localScale, new Vector3(1, 1, 1), 0.5f);
            else
            {
                if (capsuleGraphics.localScale.y >= 0.9f)
                    capsuleGraphics.localScale = new Vector3(1, 1, 1);
            }
            if (capsuleGraphics.localPosition.y < 0)
                capsuleGraphics.localPosition = Vector3.Lerp(capsuleGraphics.localPosition, new Vector3(0, 0, 0), 0.5f);
            else
            {
                if (capsuleGraphics.localPosition.y >= -0.1f)
                    capsuleGraphics.localPosition = new Vector3(0, 0, 0);
            }
            //
            if (characterController.height < 2)
                characterController.height = Mathf.Lerp(characterController.height, 2, 0.5f);
            else
            {
                if (characterController.height >= 1.9f)
                    characterController.height = 2;
            }
            if (characterController.center.y < 0)
                characterController.center = Vector3.Lerp(characterController.center, new Vector3(0, 0, 0), 0.5f);
            else
            {
                if (characterController.center.y >= -0.1f)
                    characterController.center = new Vector3(0, 0, 0);
            }
            if (Vector3.Distance(firstPersonCamera.transform.position, standingCameraPosition) > 0.5f)
            {
                firstPersonCamera.transform.position = Vector3.Lerp(firstPersonCamera.transform.position, standingCameraPosition, 0.5f);
            }
            else
            {
                if (Vector3.Distance(firstPersonCamera.transform.position, standingCameraPosition) <= 0.5f)
                    firstPersonCamera.transform.position = standingCameraPosition;
            }
        }
    }
}
