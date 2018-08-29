using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    float yaw;
    float pitch;
    public float mousesens = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchminmax = new Vector2(-45, 85);
    public float rotationSmoothtime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    
	
	void LateUpdate ()
    {
        yaw += Input.GetAxis("Mouse X") * mousesens;
        pitch -= Input.GetAxis("Mouse Y") * mousesens;
        pitch = Mathf.Clamp(pitch, pitchminmax.x, pitchminmax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(yaw, pitch), ref rotationSmoothVelocity, rotationSmoothtime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;

	}
}
