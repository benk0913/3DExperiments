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
    Rigidbody m_Rigidbody;

    [SerializeField]
    Animator m_Anim;

    [SerializeField]
    Transform SpineBone;

    [SerializeField]
    Transform NeckBone;

    [SerializeField]
    float MaxSpineBend = 60f;

    [SerializeField]
    float MinSpineBend = -60f;

    [SerializeField]
    float AnimationTraverseSpeed = 1f;

    float CurrentRotationX;

    private void FixedUpdate()
    {
        if(Input.GetKey(InputMap.Map["Forward"]))
        {
            MoveForward();
            m_Anim.SetFloat("WalkDirY", Mathf.Lerp(m_Anim.GetFloat("WalkDirY"), 1f, Time.deltaTime * AnimationTraverseSpeed));
        }
        else if (Input.GetKey(InputMap.Map["Backward"]))
        {
            MoveBackward();
            m_Anim.SetFloat("WalkDirY", Mathf.Lerp(m_Anim.GetFloat("WalkDirY"), -1f, Time.deltaTime * AnimationTraverseSpeed));
        }
        else
        {
            m_Anim.SetFloat("WalkDirY", Mathf.Lerp(m_Anim.GetFloat("WalkDirY"), 0f, Time.deltaTime * AnimationTraverseSpeed));
        }

        if (Input.GetKey(InputMap.Map["Left"]))
        {
            MoveLeft();
            m_Anim.SetFloat("WalkDirX", Mathf.Lerp(m_Anim.GetFloat("WalkDirX"), -1f, Time.deltaTime * AnimationTraverseSpeed));
        }
        else if (Input.GetKey(InputMap.Map["Right"]))
        {
            MoveRight();
            m_Anim.SetFloat("WalkDirX", Mathf.Lerp(m_Anim.GetFloat("WalkDirX"), 1f, Time.deltaTime * AnimationTraverseSpeed));
        }
        else
        {
            m_Anim.SetFloat("WalkDirX", Mathf.Lerp(m_Anim.GetFloat("WalkDirX"), 0f, Time.deltaTime * AnimationTraverseSpeed));
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
        
        transform.Rotate(Input.GetAxis("Mouse X") * InputMap.MouseSensitivityX * Vector3.up);

        CurrentRotationX += -Input.GetAxis("Mouse Y") * InputMap.MouseSensitivityY;
        CurrentRotationX = Mathf.Clamp(CurrentRotationX, MinSpineBend, MaxSpineBend);

        SpineBone.rotation = Quaternion.Euler(CurrentRotationX, SpineBone.rotation.eulerAngles.y, SpineBone.rotation.eulerAngles.z);
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
