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

    [SerializeField]
    Transform NeckBone;

    private void FixedUpdate()
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

        if(Input.GetMouseButtonDown(0))
        {
            m_Anim.SetBool("Charging", true);
            m_Anim.SetTrigger("Charge");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_Anim.SetBool("Charging", false);
            m_Anim.SetTrigger("Hit");
        }
        
        transform.Rotate(Input.GetAxis("Mouse X") * MouseXSensitivity * Vector3.up);


        if (Input.GetAxis("Mouse Y") > 0f && SpineBone.rotation.x > -0.25f)
        {
            SpineBone.transform.Rotate((Input.GetAxis("Mouse Y") * MouseYSensitivity * -Vector3.right));
        }
        else if (Input.GetAxis("Mouse Y") < 0f && SpineBone.rotation.x < 0.25f)
        {
            SpineBone.transform.Rotate((Input.GetAxis("Mouse Y") * MouseYSensitivity * -Vector3.right));
        }

        if (Input.GetAxis("Mouse Y") > 0f && NeckBone.rotation.x > -0.5f)
        {
            NeckBone.transform.Rotate((Input.GetAxis("Mouse Y") * MouseYSensitivity * -Vector3.right));
        }
        else if (Input.GetAxis("Mouse Y") < 0f && NeckBone.rotation.x < 0.5f)
        {
            NeckBone.transform.Rotate((Input.GetAxis("Mouse Y") * MouseYSensitivity * -Vector3.right));
        }
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
