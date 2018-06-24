using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float MovementSpeed = 1f;

    [SerializeField]
    float JumpForce = 1f;

    [SerializeField]
    float MouseXSensitivity = 1f;

    [SerializeField]
    float MouseYSensitivity = 1f;

    [SerializeField]
    Rigidbody m_Rigidbody;

    [SerializeField]
    Animator m_Anim;

    [SerializeField]
    Transform SpineBone;

    private void Update()
    {
        if(Input.GetKey(InputMap.Map["Forward"]))
        {
            MoveForward();
            m_Anim.SetBool("WalkForward", true);
            m_Anim.SetBool("WalkBackward", false);
        }
        else if (Input.GetKey(InputMap.Map["Backward"]))
        {
            MoveBackward();
            m_Anim.SetBool("WalkBackward", true);
            m_Anim.SetBool("WalkForward", false);
        }
        else
        {
            m_Anim.SetBool("WalkForward", false);
            m_Anim.SetBool("WalkForward", false);
        }

        if (Input.GetKey(InputMap.Map["Left"]))
        {
            MoveLeft();
            m_Anim.SetBool("WalkLeft", true);
            m_Anim.SetBool("WalkRight", false);
        }
        else if (Input.GetKey(InputMap.Map["Right"]))
        {
            MoveRight();
            m_Anim.SetBool("WalkLeft", false);
            m_Anim.SetBool("WalkRight", true);
        }
        else
        {
            m_Anim.SetBool("WalkLeft", false);
            m_Anim.SetBool("WalkRight", false);
        }

        transform.Rotate((Input.GetAxis("Mouse X") * MouseXSensitivity * Vector3.up));

        SpineBone.transform.Rotate((Input.GetAxis("Mouse Y") * MouseYSensitivity * -Vector3.right));
    }

    private void MoveRight()
    {
        m_Rigidbody.position += transform.right * MovementSpeed * Time.deltaTime;
    }

    private void MoveLeft()
    {
        m_Rigidbody.position += -transform.right * MovementSpeed * Time.deltaTime;
    }

    private void MoveBackward()
    {
        m_Rigidbody.position += -transform.forward * MovementSpeed * Time.deltaTime;
    }

    private void MoveForward()
    {
        m_Rigidbody.position += transform.forward * MovementSpeed * Time.deltaTime;
    }
}
