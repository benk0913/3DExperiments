using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    [SerializeField]
    float LookBorderHigh = 60f;

    [SerializeField]
    float LookBorderLow = -60f;

    float CurrentRotationX;

    private void FixedUpdate()
    {
        CurrentRotationX += -Input.GetAxis("Mouse Y") * InputMap.MouseSensitivityY;
        CurrentRotationX = Mathf.Clamp(CurrentRotationX, LookBorderLow, LookBorderHigh);

        transform.rotation = Quaternion.Euler(CurrentRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
    }
}
