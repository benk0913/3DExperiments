using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    [SerializeField]
    Transform TargetY;

    [SerializeField]
    Transform TargetX;

    [SerializeField]
    float MimicSpeed = 5f;

    [SerializeField]
    Vector3 PosFix;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, TargetY.transform.position + TargetY.transform.TransformDirection(PosFix), MimicSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(TargetX.transform.rotation.eulerAngles.x, TargetY.transform.rotation.eulerAngles.y,0f), MimicSpeed * Time.deltaTime);
    }
}
