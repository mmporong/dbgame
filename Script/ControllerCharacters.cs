using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCharacters : Monobehaivior
{

    #region Variables
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 5f;

    public float gravity = -9.81f;
    public Vector3 drags;

    private CharacterController characterController;


    private Vector3 inputDirection = Vector3.zero;

    private bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;

    private Vector3 calcVelocity;

    // Start is called before the first frame update

    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && calcVelocity.y < 0)
        {
            calcVelocity = 0;
        }

        // Process move inputs

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);
        if (move != Vector3.zero)
        {
            transform.forward = inputDirection;
        }
                
        // Process jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // 점프에 대한 계산공식
            calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        // Process dash input

        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * drags.x + 1)) / -Time.deltaTime),
             0,
             (Mathf.Log(1f / (Time.deltaTime * drags.z + 1)) / -Time.deltaTime)));
            calcVelocity += dashVelocity;
        }


        // Progress gravity
        calcVelocity.y += gravity * Time.deltaTime;

        // Process dash ground drags
        calcVelocity.x /= 1 + drags.x * Time.deltaTime;
        calcVelocity.y /= 1 + drags.y * Time.deltaTime;
        calcVelocity.z /= 1 + drags.z * Time.deltaTime;

        characterController.Move(calcVelocity * Time.deltaTime);
    }

    
    #region Helper Methods
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f),
        transform.position + (Vector3.up * 0.1f) + (Vector3.down * grounCheckDistance));
#endif
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f),
            Vector3.down, out hitInfo, groundCheckDistance, groundLayerMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
    #endregion Helper Methods
}
