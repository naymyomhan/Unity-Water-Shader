using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform camera;

    public float gravity = -9.81f;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    Vector3 velocity;

    void Update(){
        Debug.Log(velocity.y);

        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        if(isGrounded && velocity.y < 0 ){
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized; 

        if(direction.magnitude >=0.1f){

            float targetAngle = Mathf.Atan2(direction.x,direction.z) *  Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDirection = Quaternion.Euler(0f,targetAngle,0f)* Vector3.forward;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus){
            Cursor.lockState = CursorLockMode.Locked;
        }else{
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
