﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Animator : MonoBehaviour
{

    public AudioClip runSound, jumpSound;
    public AudioSource audio;
    public Animator animator;

    public enum Direction
    {
        Stationary, Forward, Backward, Left, Right,
        LeftForward, RightForward, LeftBackward, RightBackward
    }

    public static TP_Animator Instance;

    public Direction MoveDirection { get; set; }

    CharacterController cc;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cc = GetComponent<CharacterController>();
        animator = FindObjectOfType<Animator>();
    }

    public void DetermineCurrentMoveDirection()
    {
        var forward = false;
        var backward = false;
        var left = false;
        var right = false;

        if(TP_Motor.Instance.MoveVector.z > 0)
            forward = true;
        if (TP_Motor.Instance.MoveVector.z < 0)
            backward = true;
        if (TP_Motor.Instance.MoveVector.x > 0)
            right = true;
        if (TP_Motor.Instance.MoveVector.x < 0)
            left = true;

        if(forward)
        {
            if (left)
                MoveDirection = Direction.LeftForward;
            else if (right)
                MoveDirection = Direction.RightForward;
            else
                MoveDirection = Direction.Forward;
        }
        else if(backward)
        {
            if (left)
                MoveDirection = Direction.LeftBackward;
            else if (right)
                MoveDirection = Direction.RightBackward;
            else
                MoveDirection = Direction.Backward;
        }
        else if(left)
            MoveDirection = Direction.Left;
        else if(right)
            MoveDirection = Direction.Right;
        else
            MoveDirection = Direction.Stationary;


        if (forward && cc.isGrounded)
        {
            if (audio.isPlaying == false)
            {
                Debug.Log("is running");
                audio.clip = runSound;
                audio.Play();
                
            }
            //animator.SetTrigger("running");
        }
        else if(audio.clip == runSound)
        {
            audio.Stop();
        }
        //else if(MoveDirection == Direction.Stationary)
        //{
        //    animator.SetTrigger("stopped");
        //}

    }
}
