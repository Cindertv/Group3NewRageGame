using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkspeed = 2;
    public float runspeed = 6;
    Animator anim;

    public float turnSmoothTime = 0.02f;
    float turnSmoothVelocity;

    public float SpeedSmoothTime = 0.01f;
    float speedSmoothVelocity;
    float currentspeed;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update ()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputdirection = input.normalized;
        if (inputdirection!= Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputdirection.x, inputdirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = ((running) ? runspeed : walkspeed)* inputdirection.magnitude;
        currentspeed = Mathf.SmoothDamp(currentspeed, targetSpeed, ref speedSmoothVelocity, SpeedSmoothTime);
        transform.Translate(transform.forward * targetSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : 5f) * inputdirection.magnitude;
        anim.SetFloat("SpeedPercent", animationSpeedPercent , SpeedSmoothTime, Time.deltaTime);

    }
}
