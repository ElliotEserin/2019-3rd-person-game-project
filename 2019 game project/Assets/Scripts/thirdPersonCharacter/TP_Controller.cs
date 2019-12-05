using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Controller : MonoBehaviour
{
    public static CharacterController CharacterController;
    public static TP_Controller Instance;
    public static PuzzleController PuzzleController;



    bool inControl = true;

    void Awake()
    {
        CharacterController = GetComponent("CharacterController") as CharacterController;
        PuzzleController = FindObjectOfType<PuzzleController>();
        Instance = this;
        TP_Camera.UseExistingOrCreateNewMainCamera();

    }

    void Update()
    {
        if (transform.parent == null)
            DoUpdate();
    }

    void DoUpdate()
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

    public void Jump()
    {
        TP_Motor.Instance.Jump();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bouncePad")
        {
            var oldJumpSpeed = TP_Motor.Instance.JumpSpeed;
            TP_Motor.Instance.JumpSpeed = other.gameObject.GetComponent<BouncePad>().newJumpSpeed;

            inControl = false;
            Jump();
            TP_Motor.Instance.UpdateMotor();
            Debug.Log("jumped: at speed " + TP_Motor.Instance.JumpSpeed);

            TP_Motor.Instance.JumpSpeed = oldJumpSpeed;
            inControl = true;
        }

        if(other.gameObject.tag == "platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "platform")
        {   
            DoUpdate();
        }
       

        else if (other.gameObject.tag == "powerCell")
        {
            Destroy(other.gameObject);
            PuzzleController.cellPickup();
        }

        else if (other.gameObject.tag == "emitter")
        {
            if (PuzzleController.cellCount > 0 && other.gameObject.GetComponent<LinePuzzle>() != null)
            {
                PuzzleController.cellDeposit();
                other.gameObject.GetComponent<LinePuzzle>().enabled = true;
            }
        }

        else if(other.gameObject.tag == "obscurrence")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent = null;
    }

}