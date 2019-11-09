using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Controller : MonoBehaviour
{
    public static CharacterController CharacterController;
    public static TP_Controller Instance;


    void Awake()
    {
        CharacterController = GetComponent("CharacterController") as CharacterController;
        Instance = this;
        TP_Camera.UseExistingOrCreateNewMainCamera();
    }

    void Update()
    {
        if(Camera.main == null)
        {
            return;
        }
        GetLocomotionInput();
        HandleActionInput();

        TP_Motor.Instance.UpdateMotor();
    }

    void GetLocomotionInput()
    {
        var deadZone = 0.1f;

        TP_Motor.Instance.VerticalVelocity = TP_Motor.Instance.MoveVector.y;
        TP_Motor.Instance.MoveVector = Vector3.zero;

        if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            TP_Motor.Instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            TP_Motor.Instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"),0,0);

        TP_Animator.Instance.DetermineCurrentMoveDirection();
    }

    void HandleActionInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        TP_Motor.Instance.Jump();
    }
}