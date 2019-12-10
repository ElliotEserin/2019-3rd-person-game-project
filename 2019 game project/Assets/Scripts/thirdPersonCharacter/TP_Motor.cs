using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Motor : MonoBehaviour
{

    public static TP_Motor Instance;

    public float ForwardSpeed = 10f;
    public float BackwardSpeed = 6f;
    public float StrafingSpeed = 8f;
    public float JumpSpeed = 6f;
    public float Gravity = 21f;
    public float TerminalVelocity = 20f;
    public Animator animator;


    public Vector3 MoveVector { get; set; }
    public float VerticalVelocity { get; set; }

    TP_Controller TP_Controller;
    TP_Animator Animator;


    void Awake()
    {
        Instance = this;
        TP_Controller = FindObjectOfType<TP_Controller>();
        Animator = GetComponent<TP_Animator>();
    }

    public void UpdateMotor()
    {
        SnapAlignCharacterWithCamera();
        ProcessMotion();
        if(MoveVector.y < 0)
        {
            animator.SetTrigger("landed");
        }
    }

    void ProcessMotion()
    {
        //Transform MoveVector to World Space
        MoveVector = transform.TransformDirection(MoveVector);

        //Normalise MoveVector if Magnitude > 1
        if(MoveVector.magnitude > 1)
        {
            MoveVector = Vector3.Normalize(MoveVector);
        }

        //Multiply MoveVector by MoveSpeed
        MoveVector *= MoveSpeed();

        // reapply VerticalVelocity MoveVector.y
        MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);

        //apply gravity
        ApplyGravity();

        //Move the Character in World Space
        TP_Controller.CharacterController.Move(MoveVector * Time.deltaTime); 

    }

    void ApplyGravity()
    {
        if(MoveVector.y > - TerminalVelocity)
        {
            MoveVector = new Vector3(MoveVector.x,MoveVector.y - Gravity * Time.deltaTime,MoveVector.z);
        }
        if (TP_Controller.CharacterController.isGrounded && MoveVector.y < -1)
        {
            MoveVector = new Vector3(MoveVector.x,-1, MoveVector.z);
        }
    }

    public void Jump()
    {
        if(TP_Controller.CharacterController.isGrounded)
        {
            VerticalVelocity = JumpSpeed;

            Animator.audio.clip = Animator.jumpSound;
            Animator.audio.Play();
            animator.SetTrigger("jumped");
        }
    }

    void SnapAlignCharacterWithCamera()
    {
        if(MoveVector.x != 0 || MoveVector.z != 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                Camera.main.transform.eulerAngles.y,
                transform.eulerAngles.z);
        }
    }

    float MoveSpeed()
    {
        var moveSpeed = 0f;

        switch (TP_Animator.Instance.MoveDirection)
        {
            case TP_Animator.Direction.Stationary:
                moveSpeed = 0;
                break;
            case TP_Animator.Direction.Forward:
                moveSpeed = ForwardSpeed;
                break;
            case TP_Animator.Direction.Backward:
                moveSpeed = BackwardSpeed;
                break;
            case TP_Animator.Direction.Left:
                moveSpeed = StrafingSpeed;
                break;
            case TP_Animator.Direction.Right:
                moveSpeed = StrafingSpeed;
                break;
            case TP_Animator.Direction.LeftForward:
                moveSpeed = ForwardSpeed;
                break;
            case TP_Animator.Direction.RightForward:
                moveSpeed = ForwardSpeed;
                break;
            case TP_Animator.Direction.LeftBackward:
                moveSpeed = BackwardSpeed;
                break;
            case TP_Animator.Direction.RightBackward:
                moveSpeed = BackwardSpeed;
                break;
        }



        return moveSpeed;
    }

}
