using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCharacter : MonoBehaviour
{

    #region Variables
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 5f;

    private new Rigidbody rigidbody;

    private Vector3 inputDirection = Vector3.zero;

    private bool isGrounded = false;
    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;


    #endregion Variables
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGroundStatus();

        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");
        if (inputDirection != Vector3.zero)
        {
            transform.forward = inputDirection;
        }

        // Process jump input

        if (inputDirection != Vector3.zero)
        {
            transform.forward = inputDirection;
        }

        // Process jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // 점프에 대한 계산공식
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);

            // 가속도로 받은 힘을 입력
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }

        // Process dash input

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime),
             0,
             (Mathf.Log(1f / (Time.deltaTime * rigidbody.drag + 1)) / -Time.deltaTime)));
            rigidbody.AddForce(dashVelocity, ForceMode.VelocityChange);
        }
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + inputDirection * speed * Time.fixedDeltaTime);
    }

    #region Helper Methods
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f),
        transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
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
