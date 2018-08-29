using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    float yaw;
    float pitch;
    public float mousesens = 10;
    public Transform target;
    public float dstFromTarget = 2;
	
	void Update ()
    {
        yaw += Input.GetAxis("Mouse X") * mousesens;
        pitch -= Input.GetAxis("Mouse Y") * mousesens;

        Vector3 targetRotation = new Vector3(yaw, pitch);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * dstFromTarget;

	}
}
