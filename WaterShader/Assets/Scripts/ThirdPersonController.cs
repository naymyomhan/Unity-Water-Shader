using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{   
#region variables
    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    public Transform camera;

    private Rigidbody _Rb;
    private Vector3 _Movement;
    public float MoveSpeed;
    public float TurnSmoothTime = 0.1f;
    float turnSmoothVelocity;
#endregion

#region unity_functions
    void Start(){
        _Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {   
        HandlePlayerInput();
    }

    private void FixedUpdate()
    {   
        HandlePlayerMovement();

        float targetAngle = Mathf.Atan2(_Movement.x,_Movement.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,TurnSmoothTime);
        transform.rotation = Quaternion.Euler(0f,angle,0f);

        // transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(_Movement,Vector3.up),RotateSpeed * Time.fixedDeltaTime); 
    }
#endregion

#region custom_functions
    private void HandlePlayerInput(){
        _Movement = Vector3.zero;

        _Movement.x = Input.GetAxis(HORIZONTAL_AXIS);
        _Movement.z = Input.GetAxis(VERTICAL_AXIS);
    }

    private void HandlePlayerMovement(){
        if(_Movement == Vector3.zero){
            return;
        }
        _Rb.MovePosition(transform.position + _Movement * Time.fixedDeltaTime * MoveSpeed);
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus){
            Cursor.lockState = CursorLockMode.Locked;
        }else{
            Cursor.lockState = CursorLockMode.None;
        }
    }
#endregion
}
