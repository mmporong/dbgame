using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCharacter : MonoBehaviour
{

    #region Variables

    private CharacterController characterController;
    private UnityEngine.AI.NavMeshAgent agent;
    private Camera camera;

    private bool isGrounded = false;

    public LayerMask groundLayerMask;
    public float groundCheckDistance = 0.3f;

    private Vector3 calcVelocity;

    #endregion Variables
    // Start is called before the first frame update

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;

        camera = Camera.main;

    }

    void Update()
    {
        // Process left button input
        if (Input.GetMouseButtonDown(0))
        {
            // Make ray from screen to world
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            // Check hit from ray
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, groundLayerMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Move our character to what we hit
                agent.SetDestination(hit.point);
            }

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                characterController.Move(agent.velocity * Time.deltaTime);

            }
            else
            {
                characterController.Move(Vector3.zero);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = agent.nextPosition;
    }

}
